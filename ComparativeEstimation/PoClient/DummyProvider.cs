using System;
using System.Collections.Generic;
using Contracts;
using System.Linq;

namespace PoClient
{
    public class DummyProvider : ICes
    {
        private readonly List<string> _stories = new List<string>();

        public void Âmelde(string id)
        {
        }

        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
        }

        public GesamtgewichtungDto Gesamtgewichtung
        {
            get
            {
                var tmp = new List<string>();
                tmp.AddRange(_stories);
                tmp.Reverse();
                return new GesamtgewichtungDto()
                {
                    Anmeldungen = 5,
                    Gewichtungen = 2,
                    Stories = tmp.ToArray()
                };
            }
        }

        public IEnumerable<VergleichspaarDto> Vergleichspaare { get; }

        public void Sprint_âlege(IEnumerable<string> stories)
        {
            foreach (var story in stories)
            {
                _stories.Add(story);
            }
        }

        public void Sprint_lösche()
        {
            _stories.Clear();
        }
    }
}
