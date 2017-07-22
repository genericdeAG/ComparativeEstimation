namespace CeRestServerNancy
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var requestHandler = new CeDomain.RequestHandler(
                                                new CeWeighting.Weighting());
            using (var server = new RESTServer(args[0], requestHandler)) {
                server.WaitForConsole();
            }
        }
    }
}