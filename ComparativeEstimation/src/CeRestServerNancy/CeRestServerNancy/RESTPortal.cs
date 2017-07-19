using System;
using System.Diagnostics;
using Nancy;

namespace CeRestServerNancy
{
    public class RESTPortal : NancyModule
    {
        public RESTPortal()
        {
            Get["/info"] = _ =>
            {
                Console.WriteLine("Liveness check");
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return $"Version {fvi.FileVersion} at {DateTime.Now}";
            };
        }
    }
}