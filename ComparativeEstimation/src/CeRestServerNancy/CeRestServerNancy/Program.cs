using System;
using System.IO;

using Mono.Unix;
using Mono.Unix.Native;

using Nancy.Hosting.Self;
using Nancy;

using System.Linq;
using System.Diagnostics;

namespace helloservice
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var endpointAddress = args[0];
            using (var host = new NancyHost(new Uri(endpointAddress)))
            {
                host.Start();

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
        }


        private static bool Is_running_on_Mono => Type.GetType("Mono.Runtime") != null;

        private static UnixSignal[] UnixTerminationSignals => new[] {
            new UnixSignal(Signum.SIGINT),
            new UnixSignal(Signum.SIGTERM),
            new UnixSignal(Signum.SIGQUIT),
            new UnixSignal(Signum.SIGHUP)
        };
    }
}