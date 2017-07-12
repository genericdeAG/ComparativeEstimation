using System;
using NUnit.Framework;

using ce.contracts;
using ce.domain;

namespace ce.domain.tests
{
    [TestFixture]
    public class test_RequestHandler
    {
        [Test]
        public void Anmelden()
        {
            var state = new RequestHandler.State();
            var sut = new RequestHandler(null, state);

            sut.Âmelde("1");
            Assert.AreEqual(1, state.Anmeldungen.Count);

            sut.Âmelde("2");
            Assert.AreEqual(2, state.Anmeldungen.Count);
        }

        [Test]
        public void Anmelden_idempotent()
        {
            var state = new RequestHandler.State();
            var sut = new RequestHandler(null, state);

            sut.Âmelde("1");
            Assert.AreEqual(1, state.Anmeldungen.Count);

            sut.Âmelde("1");
            Assert.AreEqual(1, state.Anmeldungen.Count);
        }
    }
}
