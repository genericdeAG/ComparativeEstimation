using System;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Nancy.Extensions;
using CeContracts;
using CeContracts.dto;
using System.Linq;

namespace CeRestServerNancy
{
    public class RESTPortal : NancyModule
    {
        internal static IRequestHandling requestHandler;

        public RESTPortal()
        {
            Options["{*}"] = _ => Enable_CORS(new Response());

            Post["/api/sprints"] = _ => {
                var userstories = this.Bind<string[]>();
                Console.WriteLine("RESTPortal.Sprint creation requested: {0}", string.Join(";", userstories));

                var sprintId = RESTPortal.requestHandler.Create_Sprint(userstories);

                return Enable_CORS((Response)sprintId);
            };


            Delete["/api/sprints/{sprintId}"] = p => {
                Console.WriteLine("RESTPortal.Delete sprint requested for {0}", p.sprintId);

                RESTPortal.requestHandler.Delete_Sprint(p.sprintId);

                return Enable_CORS((Response)p.sprintId.ToString());
            };


            Get["/api/sprints/{sprintId}/comparisonpairs"] = p => {
                Console.WriteLine("RESTPortal.Comparison pairs queried of {0}", p.sprintId);

                var pairs = RESTPortal.requestHandler.ComparisonPairs(p.sprintId);

                var json = new JavaScriptSerializer();
                var jsonPairs = json.Serialize(pairs);
                var response = (Response)jsonPairs;
                response.ContentType = "application/json";
                return Enable_CORS(response);
            };


            Post["/api/sprints/{sprintId}/votings"] = p =>
            {
                Console.WriteLine("RESTPortal.Voting submitted: {0}", p.sprintId);

                var voting = this.Bind<VotingDto>();
                Response response = new Response();

                RESTPortal.requestHandler.Submit_voting(p.sprintId, voting,
                                                        
                    new Action(() => response = ""),

                    new Action<InconsistentVotingDto>(iv => {
                        var json = new JavaScriptSerializer();
                        var jsonIv = json.Serialize(iv);
                        response = (Response)jsonIv;
                        response.ContentType = "application/json";
                        response.StatusCode = HttpStatusCode.UnprocessableEntity;
                }));

                return Enable_CORS(response);
            };


            Get["/api/sprints/{sprintId}/totalweighting"] = p =>
            {
                Console.WriteLine("RESTPortal.Total weighting queried of {0}", p.sprintId);

                var totalWeighting = RESTPortal.requestHandler.Get_total_weighting_for_sprint(p.sprintId);

                var json = new JavaScriptSerializer();
                var jsonTotalWeighting = json.Serialize(totalWeighting);
                var response = (Response)jsonTotalWeighting;
                response.ContentType = "application/json";
                return Enable_CORS(response);
            };


            Get["/info"] = _ => {
			    Console.WriteLine("Liveness check");
			    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			    var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return $"Version {fvi.FileVersion} at {DateTime.Now} - v170821a";
			};
        }


        private Response Enable_CORS(Response response) {
            var origin = Request.Headers["Origin"].FirstOrDefault();
            if (origin != null)
            {
                response.Headers.Add("Access-Control-Allow-Origin", origin);
                response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
                response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, GET, POST, PUT, DELETE");
            }
            return response;
        }
    }
}