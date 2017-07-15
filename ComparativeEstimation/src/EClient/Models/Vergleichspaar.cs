using Contracts;

namespace EClient.Models
{
    public class Vergleichspaar
    {
        public string A { get; set; }

        public string B { get; set; }

        public string Id { get; set; }

        public Selektion Selektion { get; set; }
    }
}
