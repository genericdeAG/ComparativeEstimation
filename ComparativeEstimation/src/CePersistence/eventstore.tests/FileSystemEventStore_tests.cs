using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace eventstore.tests
{
    [TestFixture()]
    public class FileSystemEventStore_tests
    {
        [SetUp()]
        public void Setup() {
            Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory);
        }

        [Test()]
        public void Create()
        {
            const string eventStoreFolderPath = "ESCreationTest";
            if (Directory.Exists(eventStoreFolderPath)) Directory.Delete(eventStoreFolderPath, true);

            var sut = new FilesystemEventStore(eventStoreFolderPath);
            Assert.IsTrue(Directory.Exists(eventStoreFolderPath));

            var sut2 = new FilesystemEventStore(eventStoreFolderPath);
            Assert.IsTrue(Directory.Exists(eventStoreFolderPath));
        }

        [Test]
        public void Append() {
            const string eventStoreFolderPath = "ESAppendTest";
            var e = new Event
            {
                ContextId = "ctx1",
                Name = "eventname",
                Payload = "payload1\npayload2"
            };
            var sut = new FilesystemEventStore(eventStoreFolderPath);
            sut.OnAppended += _ => { };

            var seqnum = sut.Append(e);

            var eventText = File.ReadAllLines(Path.Combine(eventStoreFolderPath, e.ContextId, seqnum + ".txt"));
            Assert.AreEqual(4, eventText.Length);
            Assert.AreEqual("eventname", eventText[0]);
            Assert.IsTrue(eventText[1].IndexOf("2017") >= 0);
            Assert.AreEqual("payload1", eventText[2]);
            Assert.AreEqual("payload2", eventText[3]);
        }

        [Test]
        public void Append_fires_events()
        {
            const string eventStoreFolderPath = "ESAppendTest";
            var e = new Event
            {
                ContextId = "ctx1",
                Name = "eventname",
                Payload = "payload1\npayload2"
            };
            var sut = new FilesystemEventStore(eventStoreFolderPath);

            Event result = null;
            sut.OnAppended += events => result = events.First();

            var seqnum = sut.Append(e);

            Assert.AreEqual(seqnum, result.SequenceNumber);
            Assert.AreEqual("ctx1", result.ContextId);
            Assert.AreEqual("eventname", result.Name);
        }

        [Test]
        public void Append_several()
        {
            const string eventStoreFolderPath = "ESAppendTest";
            if (Directory.Exists(eventStoreFolderPath)) Directory.Delete(eventStoreFolderPath, true);

            var e1 = new Event {
                ContextId = "ctx1",
                Name = "eventname",
                Payload = "payload1\npayload2"
            };
            var e2 = new Event {
                ContextId = "ctx1",
                Name = "eventname2",
                Payload = "payload"
            };
            var sut = new FilesystemEventStore(eventStoreFolderPath);
            Event[] result = null;
            sut.OnAppended += events => result = events.ToArray();

            var seqnum = sut.Append(new[]{e1, e2});

            var contextFolderPath = Path.Combine(eventStoreFolderPath, e1.ContextId);
            var filenames = Directory.GetFiles(contextFolderPath, "*.txt");
            Assert.AreEqual(2, filenames.Length);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("eventname2", result[1].Name);
        }

    }
}
