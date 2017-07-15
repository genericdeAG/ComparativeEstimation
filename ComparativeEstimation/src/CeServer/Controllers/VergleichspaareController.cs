using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CeServer.Controllers
{
    [Route("api/[controller]")]
    public class VergleichspaareController: Controller
    {
        [HttpGet]
        public IEnumerable<VergleichspaarDto> Get()
        {
            return CeDomainFactory.GetDomain().Vergleichspaare;
        }
    }
}
