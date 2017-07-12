using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CeServer.Controllers
{
    [Route("api/[controller]")]
    public class AnmeldungenController: Controller
    {
        [HttpPost("{id}")]
        public void Post(string id)
        {
            CeDomainFactory.GetDomain().Âmelde(id);
        }
    }
}
