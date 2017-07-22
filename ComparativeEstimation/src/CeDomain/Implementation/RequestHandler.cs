using System;
using System.Collections.Generic;
using System.Linq;

using CeContracts;
using CeContracts.dto;
using CeContracts.data;


namespace CeDomain
{
    public class RequestHandler : IRequestHandling
    {
        internal class State
        {
            public string[] Stories = new string[0];
            public ComparisonPair[] ComparisonPairs = new ComparisonPair[0];
            public HashSet<string> EstimatorClientRegistrations = new HashSet<string>();
            public List<TotalWeighting> EstimatorWeightings = new List<TotalWeighting>();
        }

        private readonly IWeighting weighting;
        private readonly State _state;


        public RequestHandler(IWeighting weighting) : this(weighting, new State()) { }
        internal RequestHandler(IWeighting weighting, State state) {
            this.weighting = weighting;
            _state = state;
        }


        //TODO: Sprint persistent anlegen; mehrere Sprints verwalten
        public string Create_Sprint(IEnumerable<string> stories) {
            string sprintId = Guid.NewGuid().ToString();

            Delete_Sprint(sprintId);
            _state.Stories = stories.ToArray();
            _state.ComparisonPairs = Vergleichspaare_berechnen(_state.Stories.Length).ToArray();

            return sprintId;
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


        //TODO: Sprint mit id löschen
        public void Delete_Sprint(string sprintId)
        {
            _state.Stories = new string[0];
            _state.EstimatorClientRegistrations = new HashSet<string>();
            _state.EstimatorWeightings = new List<TotalWeighting>();
            _state.ComparisonPairs = new ComparisonPair[0];
        }


        public ComparisonPairsDto ComparisonPairs(string sprintId) {
            var pairs = _state.ComparisonPairs.Select(vp => new ComparisonPairDto {
													                Id = vp.Id,
													                A = _state.Stories[vp.IndexA],
													                B = _state.Stories[vp.IndexB]
													            }).ToArray();

            return new ComparisonPairsDto { 
                SprintId = sprintId,
                Pairs = pairs
            };
        }


        //TODO: voterId auswerten, Inkonsistenzquellen bestimmen
        //TODO: Registrierung idempotent machen bzw. User zuordnen; sonst kann es dazu kommen, dass es mehr Gewichtungen gibt als Anmeldungen
        public void Submit_voting(string sprintId, VotingDto voting, Action onOk, Action<InconsistentVotingDto> onInconsistency)
        {
            this.weighting.Compute_Estimator_Weighting(voting.Weightings, _state.ComparisonPairs,
                gewichtung => {
                    _state.EstimatorWeightings.Add(gewichtung);
                    onOk();
                },
                () => {
                    onInconsistency(new InconsistentVotingDto { SprintId = sprintId, ComparisonPairId = "" });
                });
        }


        public TotalWeightingDto Get_total_weighting_for_sprint(string sprintId) {
            var totalWeighting = this.weighting.Get_Total_Weighting_From(_state.EstimatorWeightings);
            var stories = Stories_for_weighting(totalWeighting, _state.Stories);
            return new TotalWeightingDto {
                SprintId = sprintId,
                Stories = stories.ToArray(),
                NumberOfVotings = _state.EstimatorWeightings.Count()
            };
        }

        internal static IEnumerable<string> Stories_for_weighting(TotalWeighting weighting, string[] stories)
            => weighting.StoryIndizes.Select(si => stories[si]);
    }
}
