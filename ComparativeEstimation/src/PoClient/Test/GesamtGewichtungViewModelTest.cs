using Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PoClient.ViewModels;

namespace PoClient.Test
{
    [TestClass]
    public class GesamtGewichtungViewModelTest
    {
        private ICes _provider;
        private GesamtgewichtungDto _dto;
        private GesamtGewichtungViewModel _ut;

        [TestInitialize]
        public void SetUp()
        {
            _provider = Substitute.For<ICes>();
            _dto = new GesamtgewichtungDto()
            {
                Anmeldungen = 8,
                Gewichtungen = 3,
                Stories = new[] { "Story B", "Story C", "Story A" }
            };
            _provider.Gesamtgewichtung.Returns(_dto);
            _ut = new GesamtGewichtungViewModel(_provider);
        }

        [TestMethod]
        public void RefreshVotingCounterTextTest()
        {
            _ut.Refresh();

            Assert.AreEqual($"{_dto.Gewichtungen} / {_dto.Anmeldungen}", _ut.VotingCounterText);
        }

        [TestMethod]
        public void RefreshGesamtgewichtungenTest()
        {
            _ut.Refresh();
            
            Assert.AreEqual(_ut.Gesamtgewichtung.Count, _dto.Stories.Length);
            for (int i = 0; i < _dto.Stories.Length; i++)
            {
                Assert.AreEqual(_ut.Gesamtgewichtung[i], _dto.Stories[i]);
            }
        }
    }
}
