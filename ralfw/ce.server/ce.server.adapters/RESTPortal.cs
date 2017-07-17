using System;
using System.Web.Script.Serialization;
using servicehost.contract;
using ce.contracts;

namespace ce.server.adapters
{
    public static class ApplicationState
    {
        public static ICes RequestHandler;
    }


    [Service]
    public class RESTPortal
    {
        //$block:b1,b2
        [EntryPoint(HttpMethods.Post, "/Anmeldungen", InputSources.Querystring)]
        public string Anmeldungen(string id)
        {
            Console.WriteLine($"  POST.Anmeldungen({id})");

            //$block:b3
            ApplicationState.RequestHandler.Âmelde(id);
            //$block:/b3
            return "";
        }
        //$block:/b1

        [EntryPoint(HttpMethods.Get, "/Vergleichspaare", InputSources.None)]
        public string Vergleichspaare(string _)
        {
            Console.WriteLine($"  GET.Vergleichspaare()");

            var vergleichspaare = ApplicationState.RequestHandler.Vergleichspaare;

            var json = new JavaScriptSerializer();
            return json.Serialize(vergleichspaare);
        }
        //$block:/b2


        [EntryPoint(HttpMethods.Post, "/Voting", InputSources.Payload)]
        public string Voting(string jsonVoting)
        {
            Console.WriteLine($"  POST.Voting({jsonVoting})");

            var json = new JavaScriptSerializer();
            GewichtetesVergleichspaarDto[] voting = json.Deserialize<GewichtetesVergleichspaarDto[]>(jsonVoting);

            var status = "";
            ApplicationState.RequestHandler.Gewichtung_regischtriere(voting,
                 () => status = "200",
                 () => status = "422");

            return status;
        }


        [EntryPoint(HttpMethods.Delete, "/Storyliste", InputSources.None)]
        public string Reset(string _)
        {
            Console.WriteLine($"  DEL.Storyliste");

            ApplicationState.RequestHandler.Sprint_lösche();
            return "";
        }


        [EntryPoint(HttpMethods.Post, "/Sprint", InputSources.Payload)]
        public string Sprint(string jsonStories)
        {
            Console.WriteLine($"  POST.Sprint({jsonStories})");

            var json = new JavaScriptSerializer();
            string[] stories = json.Deserialize<string[]>(jsonStories);

            ApplicationState.RequestHandler.Sprint_âlege(stories);

            return "";
        }


        [EntryPoint(HttpMethods.Get, "/Gesamtgewichtung", InputSources.None)]
        public string Gesamtgewichtung(string _)
        {
            Console.WriteLine($"  GET.Gesamtgewichtung");

            var gesamtgewichtung = ApplicationState.RequestHandler.Gesamtgewichtung;

            var json = new JavaScriptSerializer();
            return json.Serialize(gesamtgewichtung);
        }
    }   
}
