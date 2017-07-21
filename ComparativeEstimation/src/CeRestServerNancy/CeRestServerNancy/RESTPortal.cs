using System;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using CeContracts;
using CeContracts.dto;

namespace CeRestServerNancy
{
    public class RESTPortal : NancyModule
    {
        internal static IRequestHandling requestHandler;

        public RESTPortal()
        {
            Post["/api/sprints"] = _ => {
                var userstories = this.Bind<string[]>();
                Console.WriteLine("RESTPortal.Sprint creation requested: {0}", string.Join(";", userstories));

                var sprintId = RESTPortal.requestHandler.Create_Sprint(userstories);

                return sprintId;
            };


            Get["/api/comparisonpairs/{sprintId}"] = p => {
                Console.WriteLine("RESTPortal.Comparison pairs requested: {0}", p.sprintId);

                var pairs = RESTPortal.requestHandler.ComparisonPairs(p.sprintId);

                var json = new JavaScriptSerializer();
                var jsonPairs = json.Serialize(pairs);
                var response = (Response)jsonPairs;
                response.ContentType = "application/json";
                return response;
            };


            Post["/api/votings/{sprintId}"] = p =>
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

                return response;
            };


            Get["/info"] = _ => {
			    Console.WriteLine("Liveness check");
			    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			    var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			    return $"Version {fvi.FileVersion} at {DateTime.Now}";
			};
        }
    }
}