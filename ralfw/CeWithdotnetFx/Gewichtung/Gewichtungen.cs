using NUnit.Framework;
using System;
using System.Collections.Generic;
using Contracts;

namespace Gewichtung
{
    public class Gewichtungen : IGewichtung
    {
        public Contracts.Gewichtung Gesamtgewichtung_berechne(IEnumerable<Contracts.Gewichtung> gewichtungen)
        {
            throw new NotImplementedException();
        }

        public void Gewichtung_berechne(IEnumerable<GewichtetesVergleichspaarDto> voting, IEnumerable<Vergleichspaar> vergleichspaare, Action<Contracts.Gewichtung> ok, Action fehler)
        {
            throw new NotImplementedException();
        }
    }

}