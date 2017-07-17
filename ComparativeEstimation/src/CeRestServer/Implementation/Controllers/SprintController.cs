using System.Collections.Generic;
using System.Web.Http;

namespace Implementation2.Controllers
{
    public class SprintController : ApiController
    {
        public void Post([FromBody]IEnumerable<string> stories)
        {
            CeDomainFactory.GetDomain().Create_Sprint(stories);
        }

        public void Delete()
        {
            CeDomainFactory.GetDomain().Delete_Sprint();
        }
    }
}
