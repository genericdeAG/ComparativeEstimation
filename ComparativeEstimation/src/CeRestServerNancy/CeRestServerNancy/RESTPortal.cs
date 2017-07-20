using System;
using System.Diagnostics;
using Nancy;
using Nancy.ModelBinding;
using CeContracts;

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


            Get["/info"] = _ => {
			    Console.WriteLine("Liveness check");
			    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			    var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			    return $"Version {fvi.FileVersion} at {DateTime.Now}";
			};
        }
    }
}