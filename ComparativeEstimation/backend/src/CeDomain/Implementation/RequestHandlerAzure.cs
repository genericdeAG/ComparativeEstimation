using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeContracts;
using CeContracts.dto;

namespace CeDomain
{
    public class RequestHandlerAzure: RequestHandler
    {
        public RequestHandlerAzure(IWeighting weighting) : base(weighting)
        {
        }
    }
}
