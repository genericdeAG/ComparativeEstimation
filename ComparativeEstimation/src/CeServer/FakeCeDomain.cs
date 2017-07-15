using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;

namespace CeServer
{
    public class FakeCeDomain: ICes
    {
        public void Âmelde(string id)
        {
            
        }

        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
            fehler?.Invoke();
        }

        public GesamtgewichtungDto Gesamtgewichtung => new GesamtgewichtungDto()
        {
            Anmeldungen = 4,
            Gewichtungen = 2,
            Stories = new string[] {"a", "b", "c"}
        };

        public IEnumerable<VergleichspaarDto> Vergleichspaare
        {
            get
            {
                var vergleichspaare = new List<VergleichspaarDto>
                {
                    new VergleichspaarDto
                    {
                        A = "story1",
                        B = "story2",
                        Id =  "1"
                    }
                };

                return vergleichspaare;
            }
        }
        public void Sprint_âlege(IEnumerable<string> stories)
        {
            var testStories = stories;
        }

        public void Sprint_lösche()
        {
            
        }
    }
}
