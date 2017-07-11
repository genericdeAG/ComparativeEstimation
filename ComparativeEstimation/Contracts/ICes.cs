using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface ICes
    {
        void Anmeldung(string id);

        void GewichteteVergleichspaare(IEnumerable<GewichtetesVergleichspaar> gewichteteVergleichspaare, Action ok, Action fehler);

        Gesamtgewichtung Gesamtgewichtung();

        void Storyliste(IEnumerable<string> stories);
    }

    public class Gesamtgewichtung
    {
        public string[] Stories { get; set; }

        public int Anmeldungen { get; set; }

        public int Gewichtungen { get; set; }
    }

    public class GewichtetesVergleichspaar
    {
        public string Id { get; set; }

        public Selection Selection { get; set; }
    }

    public enum Selection
    {
        A,
        B
    }
}