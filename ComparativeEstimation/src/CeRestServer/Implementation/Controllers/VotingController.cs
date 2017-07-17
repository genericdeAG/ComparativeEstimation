using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using CeContracts;

namespace Implementation2.Controllers
{
    public class VotingController : ApiController
    {
        public void Post([FromBody] IEnumerable<WeightedComparisonPairDto> voting)
        {
            CeDomainFactory.GetDomain().Register_Estimator_Weighting(voting,
                () => { return; },
                () => { HttpContext.Current.Response.StatusCode = 422; });
        }
    }
}
