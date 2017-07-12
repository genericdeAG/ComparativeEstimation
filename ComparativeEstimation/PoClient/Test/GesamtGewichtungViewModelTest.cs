using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using NSubstitute;
using PoClient.ViewModels;
using Xunit;

namespace PoClient.Test
{
    public class GesamtGewichtungViewModelTest
    {
        [Fact]
        public void RefreshVotingCounterTextTest()
        {
            var provider = Substitute.For<ICes>();
            var dto = GetTestDto();
            provider.Gesamtgewichtung.Returns(dto);

            var ut = new GesamtGewichtungViewModel(provider);
            ut.Refresh();

            Assert.Equal($"{dto.Gewichtungen} / {dto.Anmeldungen}", ut.VotingCounterText);
        }

        [Fact]
        public void RefreshGesamtgewichtungenTest()
        {
            var provider = Substitute.For<ICes>();
            var dto = GetTestDto();
            provider.Gesamtgewichtung.Returns(dto);

            var ut = new GesamtGewichtungViewModel(provider);
            ut.Refresh();
            
            Assert.Equal(ut.Gesamtgewichtung.Count, dto.Stories.Length);
            for (int i = 0; i < dto.Stories.Length; i++)
            {
                Assert.Equal(ut.Gesamtgewichtung[i], dto.Stories[i]);
            }
        }

        private GesamtgewichtungDto GetTestDto()
        {
            return new GesamtgewichtungDto()
            {
                Anmeldungen = 8,
                Gewichtungen = 3,
                Stories = new[] { "Story B", "Story C", "Story A" }
            };
        }
    }
}
