using System;
using System.Collections.Generic;
using System.Linq;

using CeContracts;
using CeContracts.dto;
using CeContracts.data;
using CeRepository;
using eventstore;

namespace CeDomain
{
    public class RequestHandler : IRequestHandling
    {
        private readonly SprintRepository repo;
        private readonly IWeighting weighting;


        public RequestHandler(IWeighting weighting) : this(weighting, new FilesystemEventStore()) { }
        internal RequestHandler(IWeighting weighting, IEventStore eventstore) {
            this.weighting = weighting;
            this.repo = new SprintRepository(eventstore);
        }


        public string Create_Sprint(IEnumerable<string> stories) {
            var sprint = this.repo.Create(stories.ToArray());
            return sprint.Id;
        }


        public void Delete_Sprint(string sprintId) {
            this.repo.Delete(sprintId);
        }


        public ComparisonPairsDto ComparisonPairs(string sprintId) {
            var sprint = this.repo.Load(sprintId);
            var vergleichspaare = Vergleichspaare_berechnen(sprint.UserStories.Length);
            var benannteVergleichspaare = Vergleichspaare_benennen(vergleichspaare, sprint.UserStories);
            return new ComparisonPairsDto { 
                SprintId = sprintId,
                Pairs = benannteVergleichspaare.ToArray()
            };
        }

        internal static IEnumerable<ComparisonPair> Vergleichspaare_berechnen(int numberOfStories)
        {
            for (var indexA = 0; indexA < numberOfStories; indexA++)
                for (var indexB = indexA + 1; indexB < numberOfStories; indexB++)
                    yield return new ComparisonPair()
                    {
                        Id = (100 * indexA + indexB).ToString(),
                        IndexA = indexA,
                        IndexB = indexB
                    };
        }

        static IEnumerable<ComparisonPairDto> Vergleichspaare_benennen(IEnumerable<ComparisonPair> vergleichspaare, 
                                                                       string[] stories) {
            return vergleichspaare.Select(vp => new ComparisonPairDto {
										                Id = vp.Id,
										                A = stories[vp.IndexA],
										                B = stories[vp.IndexB]
										            }).ToArray();
        }


        //TODO: Inkonsistenzquellen bestimmen
        public void Submit_voting(string sprintId, VotingDto voting, Action onOk, Action<InconsistentVotingDto> onInconsistency)
        {
            var sprint = this.repo.Load(sprintId);
            var vergleichspaare = Vergleichspaare_berechnen(sprint.UserStories.Length);
            this.weighting.Compute_Estimator_Weighting(voting.Weightings, vergleichspaare,
                gewichtung => {
                    sprint.Register(new Voting(voting.VoterId, gewichtung.StoryIndizes.ToArray()));
                    this.repo.Store(sprint);
                    onOk();
                },
                () => {
                    onInconsistency(new InconsistentVotingDto { SprintId = sprintId, ComparisonPairId = "" });
                });
        }


        public TotalWeightingDto Get_total_weighting_for_sprint(string sprintId) {
            var sprint = this.repo.Load(sprintId);
            //TODO: Es ist unglücklich, dass Get_Total_Weighting_From() ein TotalWeighting liefert und TotalWeightings als Parameter erwartet
            var votings = sprint.Votings.Select(v => new TotalWeighting { StoryIndizes = v.UserStoryIndexes });
            var totalWeighting = this.weighting.Get_Total_Weighting_From(votings);
            var stories = Stories_for_weighting(totalWeighting, sprint.UserStories);
            return new TotalWeightingDto {
                SprintId = sprintId,
                Stories = stories.ToArray(),
                NumberOfVotings = sprint.Votings.Length
            };
        }

        internal static IEnumerable<string> Stories_for_weighting(TotalWeighting weighting, string[] stories)
            => weighting.StoryIndizes.Select(si => stories[si]);
    }
}
