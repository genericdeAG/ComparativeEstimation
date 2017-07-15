using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CeDomain;
using Contracts;



namespace CeServer
{
    public class CeDomainFactory
    {
        private static ICes _ices;
        public static ICes GetDomain()
        {
            if (_ices == null)
            {

                _ices = new RequestHandler(new Gewichtung.Gewichtung());
            }

            return _ices;
        }
    }
}
