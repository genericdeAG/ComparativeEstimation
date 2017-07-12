using System;
using System.Collections.Generic;
using Contracts;

namespace CeDomain
{
    internal class RequestHandlerState {
        public IEnumerable<Vergleichspaar> vergleichspaare;
        public HashSet<string> anmeldungen;
        public IEnumerable<Gewichtung> gewichtungen;
    }

    public class RequestHandler : ICes
    {
        private IGewichtung gewichter;

        private string[] stories;
        private RequestHandlerState state;


        public RequestHandler(IGewichtung gewichter) : this(gewichter, new RequestHandlerState()) {}
        internal RequestHandler(IGewichtung gewichter, RequestHandlerState state) {
            this.gewichter = gewichter;
            this.state = state;
        }


        public GesamtgewichtungDto Gesamtgewichtung => throw new NotImplementedException();

        public IEnumerable<VergleichspaarDto> Vergleichspaare => throw new NotImplementedException();

        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
            throw new NotImplementedException();
        }

        public void Sprint_lösche()
        {
            throw new NotImplementedException();
        }

        public void Sprint_âlege(IEnumerable<string> stories)
        {
            throw new NotImplementedException();
        }

        public void Âmelde(string id)
        {
            throw new NotImplementedException();
        }
    }
}
