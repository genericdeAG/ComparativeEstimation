using System;
using System.Collections.Generic;
using Contracts;

namespace EClient
{
    public class CesDummy : ICes
    {
        public void Âmelde(string id)
        {
            
        }

        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
            ok?.Invoke();
        }

        public GesamtgewichtungDto Gesamtgewichtung { get; }

        public IEnumerable<VergleichspaarDto> Vergleichspaare
        {
            get
            {
                return new List<VergleichspaarDto>
                {
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story2",
                        Id = "0"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story3",
                        Id = "1"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story2",
                        Id = "0"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story3",
                        Id = "1"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story2",
                        Id = "0"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story3",
                        Id = "1"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story2",
                        Id = "0"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story3",
                        Id = "1"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story2",
                        Id = "0"
                    },
                    new VergleichspaarDto
                    {
                        A = "Story1",
                        B = "Story3",
                        Id = "1"
                    }
                };
            }
        }

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
