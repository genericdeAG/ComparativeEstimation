using System;
using System.Collections.Generic;
using System.Linq;

using CeContracts;
using CeContracts.dto;
using CeContracts.data;
using CeWeighting.data;


namespace CeWeighting
{
    public class Weighting : IWeighting
    {
        public void Compute_Estimator_Weighting(IEnumerable<WeightedComparisonPairDto> voting, IEnumerable<ComparisonPair> comparisonPairs, Action<TotalWeighting> ok, Action exception)
        {
            var relations = Create_Relations(voting, comparisonPairs);
            var graph = GraphGenerator.Create_Graph(relations);
            GraphGenerator.Sort(graph,
                ok,
                exception);
        }

        public static IEnumerable<IndexTupel> Create_Relations(IEnumerable<WeightedComparisonPairDto> voting, IEnumerable<ComparisonPair> comparisonPairs)
        {
            var pairs = comparisonPairs.ToList();
            return voting.Select(v => Create_IndexTupel(v, pairs.First(p => p.Id == v.Id)));
        }


        private static IndexTupel Create_IndexTupel(WeightedComparisonPairDto v, ComparisonPair pair)
        {
            return new IndexTupel
            {
                MoreWeight = v.Selection == Selection.A ? pair.IndexA : pair.IndexB,
                LessWeight = v.Selection == Selection.A ? pair.IndexB : pair.IndexA
            };
        }

        public TotalWeighting Get_Total_Weighting_From(IEnumerable<TotalWeighting> estimatorWeightings)
        {
            if (estimatorWeightings == null || !estimatorWeightings.Any())
            {
                return new TotalWeighting { StoryIndizes = new int[0] };
            }

            var scorecard = Create_Scorecard(estimatorWeightings);

            return Create_Total_Weighting_From(scorecard);
        }

        private static Dictionary<int, int> Create_Scorecard(IEnumerable<TotalWeighting> estimatorWeightings)
        {
            var scorecard = Init_Scorecard(estimatorWeightings.First());

            foreach (var estimatorWeighting in estimatorWeightings)
            {
                var indexValue = Determine_IndexValue(estimatorWeighting);
                Increase_Scores(indexValue, scorecard);
            }

            return scorecard;
        }

        private static Dictionary<int, int> Init_Scorecard(TotalWeighting totalWeighting)
        {
            return totalWeighting.StoryIndizes.ToDictionary(k => k, v => 0);
        }

        private static Dictionary<int, int> Determine_IndexValue(TotalWeighting totalWeighting)
        {
            var indexValues = new Dictionary<int, int>();
            var storyIndices = totalWeighting.StoryIndizes.ToList();

            for (var i = 0; i < storyIndices.Count(); i++)
            {
                indexValues.Add(storyIndices[i], i + 1);
            }

            return indexValues;
        }

        private static void Increase_Scores(Dictionary<int, int> indexValues, Dictionary<int, int> scorecard)
        {
            foreach (var keyValuePair in indexValues)
            {
                scorecard[keyValuePair.Key] += keyValuePair.Value;
            }
        }

        private static TotalWeighting Create_Total_Weighting_From(Dictionary<int, int> scorecard)
        {
            return new TotalWeighting() { StoryIndizes = scorecard.OrderBy(pair => pair.Value).Select(pair => pair.Key) };
        }
    }
}
