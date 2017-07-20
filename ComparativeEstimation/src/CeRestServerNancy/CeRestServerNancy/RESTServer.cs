using System;
using System.IO;

using Mono.Unix;
using Mono.Unix.Native;

using Nancy.Hosting.Self;
using Nancy;

using CeContracts;

namespace CeRestServerNancy
{
    public class RESTServer : IDisposable
    {
        private NancyHost host;
        private string endpointAddress;

        public RESTServer(string endpointAddress, IRequestHandling requestHandler)
        {
            this.endpointAddress = endpointAddress;
            RESTPortal.requestHandler = requestHandler;

            this.host = new NancyHost(new Uri(endpointAddress));
            this.host.Start();
        }

        public void WaitForConsole() {
            Console.WriteLine("Running on {0}...", endpointAddress);
            if (Is_running_on_Mono)
            {
                Console.WriteLine("Ctrl-C to stop service host");
                UnixSignal.WaitAny(UnixTerminationSignals);
            }
            else
            {
                Console.WriteLine("ENTER to stop service host");
                Console.ReadLine();
            }
        }

        private static bool Is_running_on_Mono => Type.GetType("Mono.Runtime") != null;

        private static UnixSignal[] UnixTerminationSignals => new[] {
            new UnixSignal(Signum.SIGINT),
            new UnixSignal(Signum.SIGTERM),
            new UnixSignal(Signum.SIGQUIT),
            new UnixSignal(Signum.SIGHUP)
        };


        public void Dispose() {
            this.host?.Dispose();
        }
    }
}