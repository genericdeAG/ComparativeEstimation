using NUnit.Framework;
using CeContracts;
using CeContracts.dto;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;

namespace CeRestServerNancy.Tests
{
    [TestFixture()]
    public class test_RESTPortal
    {
        [Test()]
        public void TestCase()
        {
            var rh = new MockRequestHandler();
            using(new RESTServer("http://localhost:8080", rh)) {
                var stories = new[] { "X", "Y", "Z" };
                var json = new JavaScriptSerializer();
                var jsonStories = json.Serialize(stories);
                Console.WriteLine(jsonStories);

                var wc = new WebClient();
                wc.Headers.Add("Content-Type", "application/json");
                var result = wc.UploadString("http://localhost:8080/api/sprints", "Post", jsonStories);
                Assert.AreEqual("some id", result);
            }
        }
    }


    class MockRequestHandler : IRequestHandling
    {
        public ComparisonPairsDto ComparisonPairs(string id)
        {
            throw new NotImplementedException();
        }

        public string Create_Sprint(IEnumerable<string> stories)
        {
            Console.WriteLine("MockRequestHandler.Create sprint: {0}", string.Join(";", stories));
            return "some id";
        }

        public void Delete_Sprint(string id)
        {
            throw new NotImplementedException();
        }

        public TotalWeightingDto Get_total_weighting_for_sprint(string id)
        {
            throw new NotImplementedException();
        }

        public void Submit_voting(string sprintId, IEnumerable<WeightedComparisonPairDto> voting, Action onOk, Action<InconsistentVotingDto> onInconsistency)
        {
            throw new NotImplementedException();
        }
    }
}
