namespace CeRestServerNancy
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //TODO: produktions request handler übergeben
            using (var server = new RESTServer(args[0], null)) {
                server.WaitForConsole();
            }
        }
    }
}