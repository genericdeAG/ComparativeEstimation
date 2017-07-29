using System;
using System.Collections.Generic;
using System.IO;

namespace eventstore
{
    public class Event {
        public string Index;
        public string Name;
        public string ContextId;
        public string Payload;
        public DateTime Timestamp;
    }

    public interface IEventStore {
        void Append(Event e);
        void Append(IEnumerable<Event> events);

        IEnumerable<Event> Replay();
        IEnumerable<Event> Replay(string contextId);

        event Action<IEnumerable<Event>> OnAppended;
    }


    public class FilesystemEventStore : IEventStore
    {
        readonly string folderPath;

        public FilesystemEventStore() : this("EventStore") {}
        public FilesystemEventStore(string folderPath) {
            this.folderPath = folderPath;
            Directory.CreateDirectory(this.folderPath);
        }

        public void Append(Event e)
        {
            throw new NotImplementedException();
        }

        public void Append(IEnumerable<Event> events)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> Replay()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> Replay(string contextId)
        {
            throw new NotImplementedException();
        }

        public event Action<IEnumerable<Event>> OnAppended;
    }
}
