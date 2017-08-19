using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeRepository
{
    public interface ISprint
    {
        string Id { get; }
        string[] UserStories { get;  }
        Voting[] Votings { get; }

        void Register(Voting voting);
    }
}
