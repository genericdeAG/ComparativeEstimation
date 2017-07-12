using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CeServer.Controllers
{
    [Route("api/[controller]")]
    public class SprintController: Controller
    {
        [HttpPost]
        public void Post([FromBody]IEnumerable<string> stories)
        {
            CeDomainFactory.GetDomain().Sprint_âlege(stories);
        }

        [HttpDelete]
        public void Delete()
        {
            CeDomainFactory.GetDomain().Sprint_lösche();
        }
    }
}
