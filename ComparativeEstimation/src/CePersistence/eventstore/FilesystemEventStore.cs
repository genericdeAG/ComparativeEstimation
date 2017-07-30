using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace eventstore
{
    public class Event {
        public string SequenceNumber;
        public string Name;
        public string ContextId;
        public string Payload;
        public DateTime Timestamp = DateTime.Now;
    }

    public interface IEventStore {
        string Append(Event e);
        string[] Append(IEnumerable<Event> events);

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


        public string[] Append(IEnumerable<Event> events) {
            events.ToList().ForEach(Write);
            OnAppended(events);
            return events.Select(e => e.SequenceNumber).ToArray();
        }

        public string Append(Event e) {
            Write(e);
            OnAppended(new[]{e});
            return e.SequenceNumber;
        }

        private void Write(Event e) {
            e.SequenceNumber = Next_sequence_number();
            var contextPath = Ensure_context_folder(e.ContextId);
            var eventText = Serialize(e);
            Write(contextPath, e.SequenceNumber, eventText);
        }

        private void Write(string contextPath, string sequenceNumber, string eventText) {
            var eventPath = Path.Combine(contextPath, sequenceNumber + ".txt");
            File.WriteAllText(eventPath, eventText);
        }

        private string Ensure_context_folder(string contextId) {
            var contextPath = Path.Combine(this.folderPath, contextId);
            Directory.CreateDirectory(contextPath);
            return contextPath;
        }

        private string Serialize(Event e) {
            var eventText = new StringWriter();
            eventText.WriteLine(e.Name);
            eventText.WriteLine("{0:O}", e.Timestamp);
            eventText.WriteLine(e.Payload);
            return eventText.ToString();
        }




        public IEnumerable<Event> Replay()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> Replay(string contextId)
        {
            throw new NotImplementedException();
        }


        private string Next_sequence_number() {
            var t = DateTime.Now.Ticks;
            return string.Format("{0:00000000000000000000}", t);
        }


        public event Action<IEnumerable<Event>> OnAppended;
    }
}
