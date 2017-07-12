using System;
using System.Collections.Generic;
using Contracts;

namespace EClient.Tests
{
    public class CesDummy : ICes
    {
        public void Âmelde(string id)
        {
            throw new NotImplementedException();
        }

        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
            throw new NotImplementedException();
        }

        public GesamtgewichtungDto Gesamtgewichtung { get; }

        public IEnumerable<VergleichspaarDto> Vergleichspaare { get; }

        public void Sprint_âlege(IEnumerable<string> stories)
        {
            throw new NotImplementedException();
        }

        public void Sprint_lösche()
        {
            throw new NotImplementedException();
        }
    }
}
