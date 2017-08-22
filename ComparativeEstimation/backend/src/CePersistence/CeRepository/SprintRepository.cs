using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using eventstore;

namespace CeRepository
{
    interface IEventSource {
        IEnumerable<Event> Events { get; }
        void Clear();
    }

    public class Voting {
        public string VoterId { get; set; }
        public int[] UserStoryIndexes { get; set; }

        public Voting() {}
        public Voting(string voterId, int[] userStoryIndexes) {
            VoterId = voterId;
            UserStoryIndexes = userStoryIndexes;
        }
    }


    public class Sprint : IEventSource
    {
        public string Id { get; private set; }
        public string[] UserStories { get; private set; }
        public Voting[] Votings { get => this.votings.Values.ToArray(); }

        private readonly Dictionary<string, Voting> votings = new Dictionary<string, Voting>();


        private Sprint() { }
        public Sprint(string id, string[] userStories)
        {
            this.Id = id;
            this.UserStories = userStories;

            var json = new JavaScriptSerializer();
            var userStoriesJson = json.Serialize(this.UserStories);

            this.changes.Add(new Event {
                ContextId = this.Id,
                Name = "SprintCreated",
                Payload = userStoriesJson
            });
        }


        public void Register(Voting voting) {
            this.votings[voting.VoterId] = voting;

            var json = new JavaScriptSerializer();
            var votingJson = json.Serialize(voting);

            this.changes.Add(new Event{
                ContextId = this.Id,
                Name = "VotingSubmitted",
                Payload = votingJson
            });
        }


        #region IEventSource
        List<Event> changes = new List<Event>();

        IEnumerable<Event> IEventSource.Events => this.changes;
        void IEventSource.Clear() => this.changes.Clear();
        #endregion


        #region Build from events
        internal static Sprint Build(IEnumerable<Event> events)
        {
            var json = new JavaScriptSerializer();

            var sprint = new Sprint();
            foreach (var e in events)
            {
                switch (e.Name)
                {
                    case "SprintCreated":
                        sprint.Id = e.ContextId;
                        sprint.UserStories = json.Deserialize<string[]>(e.Payload);
                        break;
                    case "VotingSubmitted":
                        var voting = json.Deserialize<Voting>(e.Payload);
                        sprint.votings[voting.VoterId] = voting;
                        break;
                    case "SprintDeleted":
                        throw new InvalidOperationException($"Cannot load sprint '{sprint.Id}'! It has been deleted.");
                }
            }
            return sprint;
        }
        #endregion
    }


    public class SprintRepository
    {
        readonly IEventStore eventStore;

        public SprintRepository(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }


        public Sprint Create(string[] userStories) {
            var sprint = new Sprint(Guid.NewGuid().ToString(), userStories);

            var es = (IEventSource)sprint;
            this.eventStore.Append(es.Events);
            es.Clear();
            return sprint;
        }


        public void Store(Sprint sprint) {
            var es = (IEventSource)sprint;
            this.eventStore.Append(es.Events);
            es.Clear();
        }


        public Sprint Load(string sprintId) {
            var events = this.eventStore.Replay(sprintId);
            return Sprint.Build(events);
        }


        public void Delete(string sprintId) {
            this.eventStore.Append(new Event{
                ContextId = sprintId,
                Name = "SprintDeleted"
            });
        }
    }
}
