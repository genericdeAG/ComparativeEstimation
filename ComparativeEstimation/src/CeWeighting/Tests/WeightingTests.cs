using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using CeContracts.dto;
using CeContracts.data;
using CeWeighting;
using CeWeighting.data;


namespace CeWeightingTests
{
    [TestFixture]
    public class WeightingTests
    {
        [Test]
        public void Compute_Estimator_Weighting()
        {
            var voting = new List<WeightedComparisonPairDto>()
            {
                new WeightedComparisonPairDto {Id = "1", Selection = Selection.A},
                new WeightedComparisonPairDto {Id = "2", Selection = Selection.B},
                new WeightedComparisonPairDto {Id = "3", Selection = Selection.B},
            };

            var paare = new List<ComparisonPair>()
            {
                new ComparisonPair {Id = "1", IndexA = 0, IndexB = 1},
                new ComparisonPair {Id = "2", IndexA = 0, IndexB = 2},
                new ComparisonPair {Id = "3", IndexA = 1, IndexB = 2}
            };

            var result = new int[0];
            var sut = new Weighting();
            sut.Compute_Estimator_Weighting(
                voting,
                paare,
                g => result = g.StoryIndizes.ToArray(),
                () => { throw new Exception(); }
            );

            Assert.AreEqual(new[] { 2, 0, 1 }, result);
        }

        [Test]
        public void Get_Total_Weighting_From_Without_Votings_Returns_Empty_List()
        {
            var sut = new Weighting();

            var result = sut.Get_Total_Weighting_From(new List<TotalWeighting>());
            Assert.AreEqual(0, result.StoryIndizes.Count());

            result = sut.Get_Total_Weighting_From(null);
            Assert.AreEqual(0, result.StoryIndizes.Count());
        }

        [Test]
        public void Compute_Estimator_Weighting_Raises_Exception()
        {
            var voting = new List<WeightedComparisonPairDto>()
            {
                new WeightedComparisonPairDto {Id = "1", Selection = Selection.A},
                new WeightedComparisonPairDto {Id = "2", Selection = Selection.B},
                new WeightedComparisonPairDto {Id = "3", Selection = Selection.A},
            };

            var paare = new List<ComparisonPair>()
            {
                new ComparisonPair {Id = "1", IndexA = 0, IndexB = 1},
                new ComparisonPair {Id = "2", IndexA = 0, IndexB = 2},
                new ComparisonPair {Id = "3", IndexA = 1, IndexB = 2}
            };

            var sut = new Weighting();
            sut.Compute_Estimator_Weighting(
                voting,
                paare,
                g => { throw new Exception("kein Fehler"); },
                () => Assert.True(true)
            );
        }


        [Test]
        public void Relations()
        {
            IEnumerable<WeightedComparisonPairDto> voting = new List<WeightedComparisonPairDto>
            {
                new WeightedComparisonPairDto
                {
                    Id = "0",
                    Selection = Selection.A
                },
                new WeightedComparisonPairDto
                {
                    Id = "1",
                    Selection = Selection.B
                },
                new WeightedComparisonPairDto
                {
                    Id = "2",
                    Selection = Selection.B
                },
            };

            IEnumerable<ComparisonPair> comparisonPairs = new List<ComparisonPair>
            {
                new ComparisonPair
                {
                    Id = "0",
                    IndexA = 0,
                    IndexB = 1,
                },
                new ComparisonPair
                {
                    Id = "1",
                    IndexA = 0,
                    IndexB = 2,
                },
                new ComparisonPair
                {
                    Id = "2",
                    IndexA = 1,
                    IndexB = 2,
                },
            };
            var result = Weighting.Create_Relations(voting, comparisonPairs).ToList();
            AssertIndize(result[0], 0, 1);
            AssertIndize(result[1], 2, 0);
            AssertIndize(result[2], 2, 1);
        }

        private void AssertIndize(IndexTupel tuple, int moreWeight, int lessWeight)
        {
            Assert.AreEqual(lessWeight, tuple.LessWeight);
            Assert.AreEqual(moreWeight, tuple.MoreWeight);
        }

        [Test]
        public void Gesamtgewichtung_berechne_richtig()
        {
            var estimatorWeightings = new List<TotalWeighting>()
            {
                new TotalWeighting() {StoryIndizes = new List<int> {2, 0, 1}},
                new TotalWeighting() {StoryIndizes = new List<int> {1, 2, 0}},
                new TotalWeighting() {StoryIndizes = new List<int> {2, 1, 0}},
            };

            var sut = new Weighting();

            var result = sut.Get_Total_Weighting_From(estimatorWeightings);

            Assert.AreEqual(new[] { 2, 1, 0 }, result.StoryIndizes);
        }
    }
}
