using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeContracts;
using eventstore;

namespace CeDomain
{
    public class NancyRequestHandler: RequestHandler
    {
        public NancyRequestHandler(IWeighting weighting) : base(weighting, new FilesystemEventStore())
        {
        }
    }
}
