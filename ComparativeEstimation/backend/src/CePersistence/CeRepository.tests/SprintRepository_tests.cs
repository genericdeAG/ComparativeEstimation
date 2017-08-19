using NUnit.Framework;
using System;
using System.IO;
using eventstore;

namespace CeRepository.tests
{
    [TestFixture()]
    public class SprintRepository_tests
    {
        [Test()]
        public void Usage_scenario()
        {
            const string eventStoreFolderPath = "SprintRepoTest";
            if (Directory.Exists(eventStoreFolderPath)) Directory.Delete(eventStoreFolderPath, true);
            var es = new FilesystemEventStore(eventStoreFolderPath);
            es.OnAppended += events => {
                foreach (var e in events)
                    Console.WriteLine(e.Name);
            };

            var sut = new SprintRepository(es);

            // Sprint anlegen
            var sprint = sut.Create(new[] { "a", "b", "c" });
            var sprintId = sprint.Id;

            // Sprint laden
            sprint = sut.Load(sprintId);
            Assert.AreEqual(new[]{"a", "b", "c"}, sprint.UserStories);

            // Voting abgeben
            sprint.Register(new Voting("v1", new[]{0,1,2}));
            sut.Store(sprint);

            // Sprint laden und Voting überprüfen
            sprint = sut.Load(sprintId);
            Assert.AreEqual(1, sprint.Votings.Length);
            Assert.AreEqual(new[]{0,1,2}, sprint.Votings[0].UserStoryIndexes);

            // weiteres Voting abgeben und bisheriges überschreiben
            sprint.Register(new Voting("v2", new[]{2,1,0}));
            sprint.Register(new Voting("v1", new[]{1,2,0}));
            sut.Store(sprint);

            // Votings überprüfen
            sprint = sut.Load(sprintId);
            Assert.AreEqual(2, sprint.Votings.Length);
            Assert.AreEqual("v1", sprint.Votings[0].VoterId);
            Assert.AreEqual(new[] { 1,2,0 }, sprint.Votings[0].UserStoryIndexes);
            Assert.AreEqual("v2", sprint.Votings[1].VoterId);
            Assert.AreEqual(new[] { 2,1,0 }, sprint.Votings[1].UserStoryIndexes);

            // Sprint löschen
            sut.Delete(sprintId);
            Assert.Throws<InvalidOperationException>(() => sut.Load(sprintId));
        }
    }
}
