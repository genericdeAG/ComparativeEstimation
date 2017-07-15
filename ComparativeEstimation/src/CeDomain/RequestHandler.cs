using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace CeDomain
{
    public class RequestHandler : ICes
    {
        internal class State
        {
            public string[] Stories = new string[0];
            public Vergleichspaar[] Vergleichspaare = new Vergleichspaar[0];
            public HashSet<string> Anmeldungen = new HashSet<string>();
            public List<Gewichtung> Gewichtungen = new List<Gewichtung>();
        }

        private IGewichtung gewichter;
        private State state;


        public RequestHandler(IGewichtung gewichter) : this(gewichter, new State()) {}
        internal RequestHandler(IGewichtung gewichter, State state) {
            this.gewichter = gewichter;
            this.state = state;
        }


        public void Sprint_lösche()
        {
            this.state.Stories = new string[0];
            this.state.Anmeldungen = new HashSet<string>();
            this.state.Gewichtungen = new List<Gewichtung>();
            this.state.Vergleichspaare = new Vergleichspaar[0];
        }


        public void Sprint_âlege(IEnumerable<string> stories)
        {
            this.Sprint_lösche();
            this.state.Stories = stories.ToArray();
            this.state.Vergleichspaare = Vergleichspaare_berechnen(this.state.Stories.Length).ToArray();
        }

        internal static IEnumerable<Vergleichspaar> Vergleichspaare_berechnen(int numberOfStories)
        {
            for (var indexA = 0; indexA < numberOfStories; indexA++)
                for (var indexB = indexA + 1; indexB < numberOfStories; indexB++)
                    yield return new Vergleichspaar { 
                                        Id = (100 * indexA + indexB).ToString(), 
                                        IndexA = indexA, 
                                        IndexB = indexB };
        }


        public void Âmelde(string id)
        {
            if (this.state.Anmeldungen == null) this.state.Anmeldungen = new HashSet<string>();
            this.state.Anmeldungen.Add(id);
        }


        public IEnumerable<VergleichspaarDto> Vergleichspaare
            => this.state.Vergleichspaare.Select(vp => new VergleichspaarDto {
	                Id = vp.Id,
	                A = this.state.Stories[vp.IndexA],
	                B = this.state.Stories[vp.IndexB]
	            });


        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
            this.gewichter.Gewichtung_berechne(voting, this.state.Vergleichspaare,
               gewichtung => {
                   //TODO: Registrierung idempotent machen bzw. User zuordnen; sonst kann es dazu kommen, dass es mehr Gewichtungen gibt als Anmeldungen
                   this.state.Gewichtungen.Add(gewichtung);
                   ok();
               },
               fehler);
        }


        public GesamtgewichtungDto Gesamtgewichtung {
            get {
                var gesamtgewichung = this.gewichter.Gesamtgewichtung_berechne(this.state.Gewichtungen);
                var stories = Stories_zur_Gewichtung(gesamtgewichung, this.state.Stories);
                return new GesamtgewichtungDto { 
                    Stories = stories.ToArray(),
                    Anmeldungen = this.state.Anmeldungen.Count,
                    Gewichtungen = this.state.Gewichtungen.Count()
                };
            }
        }

        internal static IEnumerable<string> Stories_zur_Gewichtung(Gewichtung gewichtung, string[] stories)
            => gewichtung.StoryIndizes.Select(si => stories[si]);
    }
}
