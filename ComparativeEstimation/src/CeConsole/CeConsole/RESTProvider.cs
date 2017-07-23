using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;

using CeContracts;
using CeContracts.dto;

namespace CeConsole
{
    public class RESTProvider : IRequestHandling
    {
        public void Configure(string endpointAddress) {
            System.IO.File.WriteAllText("RESTProvider.config", endpointAddress);
        }

        private string EndpointAddress => System.IO.File.ReadAllText("RESTProvider.config");


        public string Create_Sprint(IEnumerable<string> stories)
        {
            var json = new JavaScriptSerializer();
            var jsonStories = json.Serialize(stories.ToArray());

            var wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            var sprintId = wc.UploadString(EndpointAddress + "/api/sprints", "Post", jsonStories);

            return sprintId;
        }


        public void Delete_Sprint(string sprintId)
        {
            var wc = new WebClient();
            wc.UploadString(EndpointAddress + $"/api/sprints/{sprintId}", "Delete", "");
        }


        public ComparisonPairsDto ComparisonPairs(string sprintId)
        {
            var wc = new WebClient();
            var resultJson = wc.DownloadString(EndpointAddress + $"/api/sprints/{sprintId}/comparisonpairs");

            var json = new JavaScriptSerializer();
            var comparisonpairs = json.Deserialize<ComparisonPairsDto>(resultJson);
            return comparisonpairs;
        }


        public void Submit_voting(string sprintId, VotingDto voting, Action onOk, Action<InconsistentVotingDto> onInconsistency)
        {
            throw new NotImplementedException();
        }


        public TotalWeightingDto Get_total_weighting_for_sprint(string sprintId)
        {
            var wc = new WebClient();
            var resultJson = wc.DownloadString(EndpointAddress + $"/api/sprints/{sprintId}/totalweighting");

            var json = new JavaScriptSerializer();
            var totalWeighting = json.Deserialize<TotalWeightingDto>(resultJson);

            return totalWeighting;
        }
    }
}
