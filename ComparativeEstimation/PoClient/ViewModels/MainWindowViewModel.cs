using System.Collections.ObjectModel;
using Contracts;
using Prism.Commands;
using Prism.Mvvm;

namespace PoClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ICes restProvider;

        public ObservableCollection<string> Stories { get; } = new ObservableCollection<string>();

        private string currentStory;

        public string CurrentStory
        {
            get => currentStory;
            set => SetProperty(ref currentStory, value);
        }
        
        public DelegateCommand AddStoryCommand { get; private set; }

        public MainWindowViewModel()
        {
            AddStoryCommand = new DelegateCommand(AddStory);
        }

        public void AddStory()
        {
            Stories.Add(CurrentStory);
        }

        public void Reset()
        {
            restProvider.Sprint_lösche();
        }
    }
}
