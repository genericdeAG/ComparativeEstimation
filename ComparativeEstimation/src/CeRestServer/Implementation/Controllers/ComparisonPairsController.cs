using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CeContracts;

namespace Implementation2.Controllers
{
    public class ComparisonPairsController : ApiController
    {
        public IEnumerable<ComparisonPairDto> Get()
        {
            return CeDomainFactory.GetDomain().ComparisonPairs;
        }
    }
}
