using System;
using System.IO;
using CeContracts.dto;
using CeWeighting;
using eventstore;
using NUnit.Framework;

namespace CeDomain.Tests
{
    [TestFixture]
    public class RequestHandlerTests
    {
        [SetUp]
        public void Setup() {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            Console.WriteLine(Environment.CurrentDirectory);
        }

        [Test]
        public void UseCase() {
            const string EVENTSTORE_FOLDER = "UseCaseEventStore";
            if (Directory.Exists(EVENTSTORE_FOLDER)) Directory.Delete(EVENTSTORE_FOLDER, true);

            var we = new Weighting();
            var es = new FilesystemEventStore(EVENTSTORE_FOLDER);
            var sut = new RequestHandler(we, es);

            // create first sprint
            var sprintId = sut.Create_Sprint(new[] { "a", "b", "c" });

            // ab, ac, bc
            var cp = sut.ComparisonPairs(sprintId);
            Assert.AreEqual(sprintId, cp.SprintId);
            Assert.AreEqual("a", cp.Pairs[0].A);
            Assert.AreEqual("b", cp.Pairs[0].B);
            Assert.AreEqual("a", cp.Pairs[1].A);
            Assert.AreEqual("c", cp.Pairs[1].B);

            var voting = new VotingDto{VoterId="v1", Weightings = new[]{
                    new WeightedComparisonPairDto{Id=cp.Pairs[0].Id, Selection=Selection.B},
                    new WeightedComparisonPairDto{Id=cp.Pairs[1].Id, Selection=Selection.B},
                    new WeightedComparisonPairDto{Id=cp.Pairs[2].Id, Selection=Selection.B}
                }};
            
            sut.Submit_voting(sprintId, voting,
	              () => { Console.WriteLine("voting sprint 1 submitted"); }, 
	              null);

            var total = sut.Get_total_weighting_for_sprint(sprintId);
            Assert.AreEqual(sprintId, total.SprintId);
            Assert.AreEqual(new[]{"c", "b", "a"}, total.Stories);
            Assert.AreEqual(1, total.NumberOfVotings);


            // create second sprint
            var sprintId2 = sut.Create_Sprint(new[] { "x", "y", "z" });
            cp = sut.ComparisonPairs(sprintId2);
            Assert.AreEqual(sprintId2, cp.SprintId);
            Assert.AreEqual("x", cp.Pairs[0].A);
            Assert.AreEqual("y", cp.Pairs[0].B);
            voting = new VotingDto {
                VoterId = "vI",
                Weightings = new[]{
                    new WeightedComparisonPairDto{Id=cp.Pairs[0].Id, Selection=Selection.A},
                    new WeightedComparisonPairDto{Id=cp.Pairs[1].Id, Selection=Selection.A},
                    new WeightedComparisonPairDto{Id=cp.Pairs[2].Id, Selection=Selection.A}
                }
            };
            sut.Submit_voting(sprintId2, voting,
                  () => { Console.WriteLine("voting sprint 2 submitted"); },
                  null);
            total = sut.Get_total_weighting_for_sprint(sprintId2);
            Assert.AreEqual(sprintId2, total.SprintId);
            Assert.AreEqual(new[] { "x", "y", "z" }, total.Stories);
            Assert.AreEqual(1, total.NumberOfVotings);


            // both sprints coexist
            cp = sut.ComparisonPairs(sprintId);
            Assert.AreEqual(sprintId, cp.SprintId);
            cp = sut.ComparisonPairs(sprintId2);
            Assert.AreEqual(sprintId2, cp.SprintId);


            // delete only second sprint
            sut.Delete_Sprint(sprintId2);
            Assert.Throws<InvalidOperationException>(() => sut.Get_total_weighting_for_sprint(sprintId2));

            cp = sut.ComparisonPairs(sprintId);
            Assert.AreEqual(sprintId, cp.SprintId);
        }
    }
}
