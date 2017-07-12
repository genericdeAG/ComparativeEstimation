using System;
using servicehost;
using servicehost.contract;
using ce.contracts;
using ce.server.adapters;

namespace ce.server
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var app = new App(() => new ce.domain.RequestHandler(
                                        new ce.gewichten.Gewichten()));
            app.Run();
        }
    }


    class App
    {
        readonly Func<ICes> newRequestHandler;

        public App(Func<ICes> newRequestHandler)
        {
            this.newRequestHandler = newRequestHandler;
        }


        public void Run()
        {
            Console.WriteLine("CE Server in ServiceHost");

            using (var host = new ServiceHost())
            {
                ApplicationState.RequestHandler = this.newRequestHandler();

                var serverAddress = "http://127.0.0.1:5000";
                host.Start(new Uri(serverAddress));
                Console.WriteLine($"  running on {serverAddress} in {System.IO.Directory.GetCurrentDirectory()}");
                Console.WriteLine("  [press any key to stop]");
                Console.ReadKey();
            }
        }
    }
}