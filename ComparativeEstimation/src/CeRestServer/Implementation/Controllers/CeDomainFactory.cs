using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CeContracts;
using CeDomain;
using CeWeighting;

namespace Implementation2.Controllers
{
    public class CeDomainFactory
    {
        private static IComparativeEstimation _comparativeEstimation;
        public static IComparativeEstimation GetDomain()
        {
            if (_comparativeEstimation == null)
            {

                _comparativeEstimation = new RequestHandler(new Weighting());
            }

            return _comparativeEstimation;
        }
    }
}