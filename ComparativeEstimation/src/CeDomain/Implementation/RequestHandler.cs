using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeContracts;

namespace CeDomain
{
    public class RequestHandler : IComparativeEstimation
    {
        internal class State
        {
            public string[] Stories = new string[0];
            public ComparisonPair[] ComparisonPairs = new ComparisonPair[0];
            public HashSet<string> EstimatorClientRegistrations = new HashSet<string>();
            public List<TotalWeighting> EstimatorWeightings = new List<TotalWeighting>();
        }

        private readonly IWeighting _weightingProcessor;
        private readonly State _state;


        public RequestHandler(IWeighting weightingProcessor) : this(weightingProcessor, new State()) { }
        internal RequestHandler(IWeighting weightingProcessor, State state)
        {
            _weightingProcessor = weightingProcessor;
            _state = state;
        }


        public void Delete_Sprint()
        {
            _state.Stories = new string[0];
            _state.EstimatorClientRegistrations = new HashSet<string>();
            _state.EstimatorWeightings = new List<TotalWeighting>();
            _state.ComparisonPairs = new ComparisonPair[0];
        }


        public void Create_Sprint(IEnumerable<string> stories)
        {
            Delete_Sprint();
            _state.Stories = stories.ToArray();
            _state.ComparisonPairs = Vergleichspaare_berechnen(_state.Stories.Length).ToArray();
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


        public void Register_Estimator_Client(string id)
        {
            if (_state.EstimatorClientRegistrations == null) _state.EstimatorClientRegistrations = new HashSet<string>();
            _state.EstimatorClientRegistrations.Add(id);
        }

        public void Register_Product_Owner_Client(string id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ComparisonPairDto> ComparisonPairs
            => _state.ComparisonPairs.Select(vp => new ComparisonPairDto
            {
                Id = vp.Id,
                A = _state.Stories[vp.IndexA],
                B = _state.Stories[vp.IndexB]
            });


        public void Register_Estimator_Weighting(IEnumerable<WeightedComparisonPairDto> voting, Action ok, Action fehler)
        {
            _weightingProcessor.Compute_Estimator_Weighting(voting, _state.ComparisonPairs,
                gewichtung =>
                {
                    //TODO: Registrierung idempotent machen bzw. User zuordnen; sonst kann es dazu kommen, dass es mehr Gewichtungen gibt als Anmeldungen
                    _state.EstimatorWeightings.Add(gewichtung);
                    ok();
                },
                fehler);
        }


        public TotalWeightingDto TotalWeighting
        {
            get
            {
                var gesamtgewichung = _weightingProcessor.Get_Total_Weighting_From(_state.EstimatorWeightings);
                var stories = Stories_zur_Gewichtung(gesamtgewichung, _state.Stories);
                return new TotalWeightingDto
                {
                    Stories = stories.ToArray(),
                    EstimatorClientRegistrations = _state.EstimatorClientRegistrations.Count,
                    EstimatorWeightings = _state.EstimatorWeightings.Count()
                };
            }
        }

        internal static IEnumerable<string> Stories_zur_Gewichtung(TotalWeighting gewichtung, string[] stories)
            => gewichtung.StoryIndizes.Select(si => stories[si]);
    }
}
