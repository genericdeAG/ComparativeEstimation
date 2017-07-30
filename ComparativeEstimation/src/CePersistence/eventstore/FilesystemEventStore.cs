using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace eventstore
{
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


        private string Next_sequence_number() {
            var t = DateTime.Now.Ticks;
            return string.Format("{0:00000000000000000000}", t);
        }



        public IEnumerable<Event> Replay() {
            var contextIds = Compile_all_contexts(this.folderPath);
            var events = Replay(contextIds);
            return Ensure_ordered_events(events);

        }

        IEnumerable<Event> Replay(IEnumerable<string> contextIds) 
            => contextIds.SelectMany(Replay_unordered);

        public IEnumerable<Event> Replay(string contextId) {
            var events = Replay_unordered(contextId);
            return Ensure_ordered_events(events);
        }

        private IEnumerable<Event> Replay_unordered(string contextId) {
            var eventFilePaths = Compile_filenames(contextId);
            return Read_events(eventFilePaths);
        }

        private IEnumerable<string> Compile_filenames(string contextId) {
            var contextFolderPath = Path.Combine(this.folderPath, contextId);
            return Directory.GetFiles(contextFolderPath, "*.txt");
        }

        private IEnumerable<Event> Read_events(IEnumerable<string> eventFilePaths) {
            return eventFilePaths.Select(Read_event);
        }

        private Event Read_event(string eventFilePath) {
            var eventText = File.ReadAllText(eventFilePath);
            var e = Deserialize(eventText, eventFilePath);
            return e;
        }


        private static string Serialize(Event e) {
            var eventText = new StringWriter();
            eventText.WriteLine(e.Name);
            eventText.WriteLine("{0:O}", e.Timestamp);
            eventText.WriteLine(e.Payload);
            return eventText.ToString();
        }

        private static Event Deserialize(string eventText, string eventFilePath) {
            var sequenceNumber = Path.GetFileNameWithoutExtension(eventFilePath);
            var contextId = Path.GetDirectoryName(eventFilePath);

            var sr = new StringReader(eventText);
            var name = sr.ReadLine();
            var timestamp = DateTime.Parse(sr.ReadLine());
            var payload = sr.ReadToEnd().TrimEnd();

            return new Event { 
                SequenceNumber = sequenceNumber,
                Name = name,
                ContextId = contextId,
                Timestamp = timestamp,
                Payload = payload
            };
        }


        static IEnumerable<Event> Ensure_ordered_events(IEnumerable<Event> events) => events.OrderBy(e => e.SequenceNumber);

        static IEnumerable<string> Compile_all_contexts(string folderPath) => Directory.GetDirectories(folderPath);


        public event Action<IEnumerable<Event>> OnAppended;
    }
}
