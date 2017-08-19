using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using eventstore;

namespace CeRepository
{
    public class SprintBlob: ISprint
    {
        public string Id { get; }
        public string[] UserStories { get;}
        public Voting[] Votings => _votings.Values.ToArray();

        private readonly Dictionary<string, Voting> _votings = new Dictionary<string, Voting>();


        private SprintBlob() { }
        public SprintBlob(string id, string[] userStories)
        {
            Id = id;
            UserStories = userStories;
        }


        public void Register(Voting voting)
        {
            _votings[voting.VoterId] = voting;
        }
    }
}
