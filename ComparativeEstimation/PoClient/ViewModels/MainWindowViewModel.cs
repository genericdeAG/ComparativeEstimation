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
        
        public DelegateCommand AddStoryCommand { get; }
        public DelegateCommand StartCommand { get; }

        public MainWindowViewModel()
        {
            AddStoryCommand = new DelegateCommand(AddStory);
            StartCommand = new DelegateCommand(Start);

            restProvider = App.Resolve<ICes>();

            Reset();
        }

        public void AddStory()
        {
            Stories.Add(CurrentStory);
        }

        public void Reset()
        {
            restProvider.Sprint_lösche();
        }

        public void Start()
        {
            restProvider.Sprint_âlege(Stories);
        }
    }
}
