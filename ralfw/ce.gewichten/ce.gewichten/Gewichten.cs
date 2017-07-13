using System;
using System.Collections.Generic;
using System.Linq;
using ce.contracts;
using ce.gewichten.model;

namespace ce.gewichten
{
    public class Gewichten : IGewichtung
    {
        public void Gewichtung_berechne(IEnumerable<GewichtetesVergleichspaarDto> voting, IEnumerable<Vergleichspaar> vergleichspaare, Action<Gewichtung> ok, Action fehler)
        {
            var relationen = Relationen_erstellen(voting, vergleichspaare);
            var graph = GraphGenerator.Graph_erstellen(relationen);
            GraphGenerator.Sortieren(graph,
                ok,
                fehler);
        }

        internal static IEnumerable<IndexTupel> Relationen_erstellen(IEnumerable<GewichtetesVergleichspaarDto> voting, IEnumerable<Vergleichspaar> vergleichspaare)
        {
            var paare = vergleichspaare.ToList();
            return voting.Select(v => ErstelleTupel(v, paare.First(p => p.Id == v.Id)));
        }

        private static IndexTupel ErstelleTupel(GewichtetesVergleichspaarDto v, Vergleichspaar paar)
        {
            return new IndexTupel
            {
                Wichtiger = v.Selektion == Selektion.A ? paar.IndexA : paar.IndexB,
                Weniger = v.Selektion == Selektion.A ? paar.IndexB : paar.IndexA
            };
        }

        public Gewichtung Gesamtgewichtung_berechne(IEnumerable<Gewichtung> gewichtungen)
        {
            if (gewichtungen == null || !gewichtungen.Any())
            {
                return new Gewichtung { StoryIndizes = new int[0]}; 
            }

            var scorecard = Scorecard_erzeugen(gewichtungen);

            return Gesamtgewichtung_erzeugen(scorecard);
        }

        private static Dictionary<int, int> Scorecard_erzeugen(IEnumerable<Gewichtung> gewichtungen)
        {
            var scorecard = InitScorecard(gewichtungen.First());

            foreach (var gewichtung in gewichtungen)
            {
                var indexwerte = Indexwerte_ermitteln(gewichtung);
                ScoresErhöhen(indexwerte, scorecard);
            }

            return scorecard;
        }

        private static Dictionary<int, int> InitScorecard(Gewichtung gewichtung)
        {
            return gewichtung.StoryIndizes.ToDictionary(k => k, v => 0);
        }

        private static Dictionary<int, int> Indexwerte_ermitteln(Gewichtung gewichtung)
        {
            var indexwerte = new Dictionary<int, int>();
            var storyIndizes = gewichtung.StoryIndizes.ToList();

            for (var i = 0; i < storyIndizes.Count(); i++)
            {
                indexwerte.Add(storyIndizes[i], i + 1);
            }

            return indexwerte;
        }

        private static void ScoresErhöhen(Dictionary<int, int> indexwerte, Dictionary<int, int> scorecard)
        {
            foreach (var keyValuePair in indexwerte)
            {
                scorecard[keyValuePair.Key] += keyValuePair.Value;
            }
        }

        private static Gewichtung Gesamtgewichtung_erzeugen(Dictionary<int, int> scorecard)
        {
            return new Gewichtung { StoryIndizes = scorecard.OrderBy(pair => pair.Value).Select(pair => pair.Key) };
        }
    }
}