using System;
using System.Collections.Generic;

namespace eventstore
{
    public interface IEventStore {
        string Append(Event e);
        string[] Append(IEnumerable<Event> events);

        IEnumerable<Event> Replay();
        IEnumerable<Event> Replay(string contextId);

        event Action<IEnumerable<Event>> OnAppended;
    }
}