using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CeServer.Controllers
{
    [Route("api/[controller]")]
    public class VotingController: Controller
    {
        public void Post([FromBody] IEnumerable<GewichtetesVergleichspaarDto> voting)
        {
            CeDomainFactory.GetDomain().Gewichtung_regischtriere(voting,
                () => { return; },
                () => { ControllerContext.HttpContext.Response.StatusCode = new StatusCodeResult(422).StatusCode; } );
        }

    }
}
