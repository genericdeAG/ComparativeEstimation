using NUnit.Framework;
using CeContracts;
using CeContracts.dto;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;
using System.Linq;
using System.IO;

namespace CeRestServerNancy.Tests
{
    [TestFixture()]
    public class test_RESTPortal
    {
        [Test()]
        public void Create_sprint()
        {
            var rh = new MockRequestHandler();
            rh.sprintId = "42";

            using(new RESTServer("http://localhost:8080", rh)) {
                var stories = new[] { "X", "Y", "Z" };
                var json = new JavaScriptSerializer();
                var jsonStories = json.Serialize(stories);

                var wc = new WebClient();
                wc.Headers.Add("Content-Type", "application/json");
                var result = wc.UploadString("http://localhost:8080/api/sprints", "Post", jsonStories);

                Assert.AreEqual("42", result);
            }
        }


        [Test]
        public void Get_comparison_pairs() {
            var rh = new MockRequestHandler();
            rh.sprintId = "some id";

            using (new RESTServer("http://localhost:8080", rh))
            {
                var wc = new WebClient();
                var resultJson = wc.DownloadString("http://localhost:8080/api/comparisonpairs/42");
                Console.WriteLine(resultJson);

                var json = new JavaScriptSerializer();
                var result = json.Deserialize<ComparisonPairsDto>(resultJson);

                Assert.AreEqual("42", result.SprintId);

                var pairs = result.Pairs.ToArray();
                Assert.AreEqual(2, result.Pairs.Length);
                Assert.AreEqual("1", result.Pairs[0].Id);
                Assert.AreEqual("Z", result.Pairs[1].B);
            }
        }


        [Test]
        public void Submit_valid_voting() {
            var rh = new MockRequestHandler();

            using (new RESTServer("http://localhost:8080", rh))
            {
                var voting = new VotingDto { 
                    VoterId = "Frodo",
                    Weightings = new[]{
                        new WeightedComparisonPairDto{
                            Id = "1", Selection = Selection.A
                        },
                        new WeightedComparisonPairDto{
                            Id = "2", Selection = Selection.B
                        }
                    }
                };
                var json = new JavaScriptSerializer();
                var jsonVoting = json.Serialize(voting);

                var wc = new WebClient();
                wc.Headers.Add("Content-Type", "application/json");
                var result = wc.UploadString("http://localhost:8080/api/votings/42", "Post", jsonVoting);

                Assert.AreEqual("42", rh.sprintId);
                Assert.AreEqual("Frodo", rh.voting.VoterId);
                Assert.AreEqual("2", rh.voting.Weightings.Last().Id);
            }
        }


        [Test]
        public void Submit_inconsistent_voting()
        {
            var rh = new MockRequestHandler();

            using (new RESTServer("http://localhost:8080", rh))
            {
                try {
                    var voting = new VotingDto {
                        VoterId = "Sauron",
                        Weightings = null
                    };
                    var json = new JavaScriptSerializer();
                    var jsonVoting = json.Serialize(voting);

                    var wc = new WebClient();
                    wc.Headers.Add("Content-Type", "application/json");
                    var result = wc.UploadString("http://localhost:8080/api/votings/42", "Post", jsonVoting);
                }
                catch(WebException e) {
                    var response = (System.Net.HttpWebResponse)e.Response;
                    Assert.AreEqual(422, (int)response.StatusCode);

                    var sr = new StreamReader(response.GetResponseStream());
                    var jsonIv = sr.ReadToEnd();
                    Console.WriteLine("Inconsistent voting: {0}", jsonIv);

                    var json = new JavaScriptSerializer();
                    var iv = json.Deserialize<InconsistentVotingDto>(jsonIv);
                    Assert.AreEqual("42", iv.SprintId);
                    Assert.AreEqual("1", iv.ComparisonPairId);
                }
            }
        }


        [Test]
        public void Get_total_weighting()
        {
            var rh = new MockRequestHandler();

            using (new RESTServer("http://localhost:8080", rh))
            {
                var wc = new WebClient();
                var resultJson = wc.DownloadString("http://localhost:8080/api/sprints/42/totalweighting");
                Console.WriteLine(resultJson);

                var json = new JavaScriptSerializer();
                var result = json.Deserialize<TotalWeightingDto>(resultJson);

                Assert.AreEqual("42", result.SprintId);
                Assert.AreEqual(new[]{"Z", "Y", "X"}, result.Stories);
                Assert.AreEqual(4, result.NumberOfVotings);
            }
        }
    }


    class MockRequestHandler : IRequestHandling
    {
        public string sprintId;
        public VotingDto voting;


        public string Create_Sprint(IEnumerable<string> stories)
        {
            Console.WriteLine("MockRequestHandler.Create sprint: {0}", string.Join(";", stories));
            return this.sprintId;
        }

        public ComparisonPairsDto ComparisonPairs(string sprintId)
        {
            Console.WriteLine("MockRequestHandler.Comparison pairs for {0}", sprintId);
            this.sprintId = sprintId;

            return new ComparisonPairsDto { 
                SprintId = sprintId,
                Pairs = new[]{
                            new ComparisonPairDto{Id="1", A="X", B="Y"},
                            new ComparisonPairDto { Id = "2", A = "X", B = "Z" } 
                        }
            };
        }


        public void Submit_voting(string sprintId, VotingDto voting, Action onOk, Action<InconsistentVotingDto> onInconsistency)
        {
            Console.WriteLine("MockRequestHandler.Submit voting for {0}", sprintId);

            this.sprintId = sprintId;
            this.voting = voting;

            if (voting.VoterId == "Sauron")
                onInconsistency(new InconsistentVotingDto { SprintId = sprintId, ComparisonPairId = "1" });
            else
                onOk();
        }


        public TotalWeightingDto Get_total_weighting_for_sprint(string sprintId)
        {
            Console.WriteLine("MockRequestHandler.Get total weighting for {0}", sprintId);
            this.sprintId = sprintId;

            return new TotalWeightingDto { 
                SprintId = sprintId,
                Stories = new[]{"Z", "Y", "X"},
                NumberOfVotings = 4
            };
        }


        public void Delete_Sprint(string id)
        {
            throw new NotImplementedException();
        }
    }
}
