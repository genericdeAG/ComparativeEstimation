using System;
using System.Collections.Generic;
using Contracts;

namespace PoClient
{
    public class DummyProvider : ICes
    {
        public void Âmelde(string id)
        {
        }

        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
        }

        public GesamtgewichtungDto Gesamtgewichtung { get; }
        public IEnumerable<VergleichspaarDto> Vergleichspaare { get; }
        public void Sprint_âlege(IEnumerable<string> stories)
        {
        }

        public void Sprint_lösche()
        {
        }
    }
}
