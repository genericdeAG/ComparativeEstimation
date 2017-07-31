using System;

namespace eventstore
{
    public class Event {
        public string SequenceNumber;
        public string Name;
        public string ContextId;
        public string Payload = "";
        public DateTime Timestamp = DateTime.Now;
    }
}