using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Implementation2.Models;

namespace Implementation2.Controllers
{
    public class EstimatorClientRegistrationController : ApiController
    {
        public void Post([FromBody]EstimatorClientRegistrationModel model)
        {
        }
    }
}
