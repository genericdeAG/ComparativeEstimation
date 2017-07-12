using System;
using System.Collections.ObjectModel;
using Contracts;
using PoClient.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace PoClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ICes _restProvider;

        public ObservableCollection<string> Stories { get; } = new ObservableCollection<string>();

        private string _currentStory;

        public string CurrentStory
        {
            get => _currentStory;
            set => SetProperty(ref _currentStory, value);
        }
        
        public Action CloseWindow { get; set; }

        public DelegateCommand AddStoryCommand { get; }
        public DelegateCommand StartCommand { get; }

        public MainWindowViewModel(ICes restProvider)
        {
            _restProvider = restProvider;

            AddStoryCommand = new DelegateCommand(AddStory);
            StartCommand = new DelegateCommand(Start);
            
            Reset();
        }

        public void AddStory()
        {
            Stories.Add(CurrentStory);
            CurrentStory = string.Empty;
        }

        public void Reset()
        {
            _restProvider.Sprint_lösche();
        }

        public void Start()
        {
            _restProvider.Sprint_âlege(Stories);
            new GesamtGewichtungView().Show();
            CloseWindow?.Invoke();
        }
    }
}
