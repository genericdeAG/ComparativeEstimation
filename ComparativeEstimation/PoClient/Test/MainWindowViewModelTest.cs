using Contracts;
using NSubstitute;
using PoClient.ViewModels;
using Xunit;

namespace PoClient.Test
{
    public class MainWindowViewModelTest
    {
        [Fact]
        public void AddStoryTest()
        {
            var provider = Substitute.For<ICes>();

            var ut = new MainWindowViewModel(provider)
            {
                CurrentStory = "New Story"
            };

            ut.AddStory();

            Assert.Equal(ut.Stories.Count,1);
            Assert.Equal(ut.Stories[0], "New Story");
            Assert.Equal(string.Empty, ut.CurrentStory);
        }
    }
}
