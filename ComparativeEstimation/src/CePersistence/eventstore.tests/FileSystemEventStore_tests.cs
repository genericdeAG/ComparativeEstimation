using NUnit.Framework;
using System;
using System.IO;

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
    }
}
