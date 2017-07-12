using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface ICes
    {
        void Âmelde(string id);

        void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler);

        GesamtgewichtungDto Gesamtgewichtung { get; }

        IEnumerable<VergleichspaarDto> Vergleichspaare { get; }

        void Sprint_âlege(IEnumerable<string> stories);

        void Sprint_lösche();
    }

    public class VergleichspaarDto
    {
        public string A { get; set; }

        public string B { get; set; }

        public string Id { get; set; }
    }

    public class Vergleichspaar
    {
        public int IndexA { get; set; }

        public int IndexB { get; set; }

        public string Id { get; set; }
    }

    public class GesamtgewichtungDto
    {
        public string[] Stories { get; set; }

        public int Anmeldungen { get; set; }

        public int Gewichtungen { get; set; }
    }

    public class GewichtetesVergleichspaarDto
    {
        public string Id { get; set; }

        public Selektion Selektion { get; set; }
    }

    public enum Selektion
    {
        A,
        B
    }

    public interface IGewichtung
    {
        void Gewichtung_berechne(IEnumerable<GewichtetesVergleichspaarDto> voting, IEnumerable<Vergleichspaar>  vergleichspaare, Action<Gewichtung> ok, Action fehler);

        Gewichtung Gesamtgewichtung_berechne(IEnumerable<Gewichtung> gewichtungen);
    }

    public class Gewichtung
    {
        public IEnumerable<int> StoryIndizes { get; set; }
    }
}