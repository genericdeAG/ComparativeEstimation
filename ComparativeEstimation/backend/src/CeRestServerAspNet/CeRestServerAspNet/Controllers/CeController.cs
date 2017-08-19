using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using CeContracts.dto;
using CeDomain;
using CeWeighting;

namespace CeRestServerAspNet.Controllers
{
    public class CeController : ApiController
    {
        private static RequestHandlerAzure _requestHandler;

        public RequestHandlerAzure RequestHandler
        {
            get
            {
                if (_requestHandler == null)
                {
                    _requestHandler = new RequestHandlerAzure(new Weighting());
                }

                return _requestHandler;
            }
        }

        [HttpGet]
        [Route("api/sprints/{sprintId}/comparisonpairs")]
        public HttpResponseMessage GetComparisonPairs(string sprintId)
        {
            var pairs = RequestHandler.ComparisonPairs(sprintId);

            var json = new JavaScriptSerializer();
            var jsonPairs = json.Serialize(pairs);
            var res = new HttpResponseMessage();
            res.Content = new StringContent(jsonPairs, Encoding.UTF8, "application/json");
            return res;
        }

        [HttpGet]
        [Route("api/sprints/{sprintId}/totalweighting")]
        public HttpResponseMessage GetTotalWeighting(string sprintId)
        {
            var totalWeighting = RequestHandler.Get_total_weighting_for_sprint(sprintId);
            var json = new JavaScriptSerializer();
            var jsontotalWeighting = json.Serialize(totalWeighting);
            var res = new HttpResponseMessage();
            res.Content = new StringContent(jsontotalWeighting, Encoding.UTF8, "application/json");
            return res;
        }

        [Route("api/sprints")]
        public HttpResponseMessage Post([FromBody]IEnumerable<string> stories)
        {
            var id = RequestHandler.Create_Sprint(stories);
            var res = new HttpResponseMessage();
            res.Content = new StringContent(id, System.Text.Encoding.UTF8, "text/plain");
            return res;

        }

        [Route("api/sprints/{sprintId}/votings")]
        public HttpResponseMessage Post(string sprintId, [FromBody]VotingDto voting)
        {
            var res = new HttpResponseMessage();
            RequestHandler.Submit_voting(sprintId, voting,
                new Action(() => res = new HttpResponseMessage()),

                new Action<InconsistentVotingDto>(iv => {
                    var json = new JavaScriptSerializer();
                    var jsonIv = json.Serialize(iv);
                    res = new HttpResponseMessage();
                    res.Content = new StringContent(jsonIv, Encoding.UTF8, "application/json");
                    res.StatusCode = (HttpStatusCode) 422;
                }));

            return res;
        }

        [Route("api/sprints/{sprintId}")]
        public void Delete(string sprintId)
        {
            RequestHandler.Delete_Sprint(sprintId);
        }
    }
}
