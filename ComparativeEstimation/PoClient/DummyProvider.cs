using System;
using System.Collections.Generic;
using Contracts;

namespace PoClient
{
    public class DummyProvider : ICes
    {
        private readonly List<string> _stories = new List<string>();

        public void Âmelde(string id)
        {
            throw new NotImplementedException();
        }

        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
            throw new NotImplementedException();
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

        public IEnumerable<VergleichspaarDto> Vergleichspaare => throw new NotImplementedException();

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
