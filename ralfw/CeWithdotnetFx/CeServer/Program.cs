using System;
using System.Web.Script.Serialization;
using servicehost;
using servicehost.contract;
using Contracts;

namespace CeServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var app = new App(() => new CeDomain.RequestHandler(new Gewichtung.Gewichtungen()));
            app.Run();
        }
    }


    class App {
        readonly Func<ICes> newRequestHandler;

        public App(Func<ICes> newRequestHandler)
        {
            this.newRequestHandler = newRequestHandler;
        }

        public void Run() {
            Console.WriteLine("CE Server");

            using (var host = new ServiceHost())
            {
                ApplicationState.RequestHandler = this.newRequestHandler();

                host.Start(new Uri("http://localhost:1234"));
                Console.WriteLine("  running");
                Console.WriteLine("  press any key to stop");
                Console.ReadKey();
            }
        }
    }


    static class ApplicationState {
        public static ICes RequestHandler;
    }


    [Service]
    internal class RESTAdapter {
        //OK
        [EntryPoint(HttpMethods.Post, "/Anmeldungen", InputSources.Querystring)]
        public string Anmeldungen(string id) {
            ApplicationState.RequestHandler.Âmelde(id);
            return "";
        }


        [EntryPoint(HttpMethods.Get, "/Vergleichspaare", InputSources.Payload)]
        public string Vergleichspaare(string _) {
            throw new NotImplementedException();
        }


        //OK
        [EntryPoint(HttpMethods.Post,"/Voting", InputSources.Payload)]
        public string Voting(string jsonVoting) {
            var json = new JavaScriptSerializer();
            GewichtetesVergleichspaarDto[] voting = json.Deserialize<GewichtetesVergleichspaarDto[]>(jsonVoting);

            var status = "";
            ApplicationState.RequestHandler.Gewichtung_regischtriere(voting, 
                 () => status = "200", 
                 () => status = "422");

            return status;
        }


        //OK
        [EntryPoint(HttpMethods.Delete, "/Storyliste", InputSources.Payload)]
        public string Reset(string _) {
            ApplicationState.RequestHandler.Sprint_lösche();
            return "";    
        }


        [EntryPoint(HttpMethods.Post, "/Sprint", InputSources.Payload)]
        public string Sprint(string json) {
            throw new NotImplementedException();
            return "";
        }


        [EntryPoint(HttpMethods.Get, "/Gesamtgewichtung", InputSources.Payload)]
        public string Gesamtgewichtung(string _) {
            throw new NotImplementedException();
            return "";
        }
    }
}