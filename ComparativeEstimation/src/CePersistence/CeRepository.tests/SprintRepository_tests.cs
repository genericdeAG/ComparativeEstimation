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
            const string eventStoreFolderPath = "ESRepoTest";
            if (Directory.Exists(eventStoreFolderPath)) Directory.Delete(eventStoreFolderPath, true);
            var es = new FilesystemEventStore(eventStoreFolderPath);
            es.OnAppended += _ => { };

            var sut = new SprintRepository(es);

            var sprint = sut.Create(new[] { "a", "b", "c" });
            var sprintId = sprint.Id;

            sprint = sut.Load(sprintId);
            Assert.AreEqual(new[]{"a", "b", "c"}, sprint.UserStories);

            sprint.Register(new Voting("v1", new[]{0,1,2}));
            sut.Store(sprint);

            sprint = sut.Load(sprintId);
            Assert.AreEqual(1, sprint.Votings.Length);
            Assert.AreEqual(new[]{0,1,2}, sprint.Votings[0].UserStoryIndexes);

            sprint.Register(new Voting("v2", new[]{2,1,0}));
            sprint.Register(new Voting("v1", new[]{1,2,0}));
            sut.Store(sprint);

            sprint = sut.Load(sprintId);
            Assert.AreEqual(2, sprint.Votings.Length);
            Assert.AreEqual("v1", sprint.Votings[0].VoterId);
            Assert.AreEqual(new[] { 1,2,0 }, sprint.Votings[0].UserStoryIndexes);
            Assert.AreEqual("v2", sprint.Votings[1].VoterId);
            Assert.AreEqual(new[] { 2,1,0 }, sprint.Votings[1].UserStoryIndexes);
        }
    }
}
