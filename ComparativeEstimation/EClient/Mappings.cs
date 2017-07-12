using Contracts;

namespace EClient
{
    public class Mappings
    {
        public static Vergleichspaar MapVergleichspaar(VergleichspaarDto vp)
        {
            return new Vergleichspaar
            {
                A = vp.A,
                B = vp.B,
                Id = vp.Id
            };
        }

        public static GewichtetesVergleichspaarDto MapGewichtetesVergleichspaar(Vergleichspaar paar)
        {
            return new GewichtetesVergleichspaarDto
            {
                Id = paar.Id,
                Selektion = paar.Selektion
            };
        }
    }
}
