using System;
using System.Collections.ObjectModel;
using Contracts;
using Prism.Commands;
using Prism.Mvvm;

namespace PoClient.ViewModels
{
    public class GesamtGewichtungViewModel : BindableBase
    {
        private readonly ICes _restProvider;

        private string _votingCounterText;

        public string VotingCounterText
        {
            get => _votingCounterText;
            set => SetProperty(ref _votingCounterText, value);
        }

        public ObservableCollection<string> Gesamtgewichtung { get; } = new ObservableCollection<string>();

        public DelegateCommand RefreshCommand { get; }

        public GesamtGewichtungViewModel(ICes restProvider)
        {
            _restProvider = restProvider;

            RefreshCommand = new DelegateCommand(Refresh);
            Refresh();
        }

        public void Refresh()
        {
            var gesamtgewichtung = _restProvider.Gesamtgewichtung ?? new GesamtgewichtungDto
            {
                Stories = Array.Empty<string>()
            };

            VotingCounterText = $"{gesamtgewichtung.Gewichtungen} / {gesamtgewichtung.Anmeldungen}";
            Gesamtgewichtung.Clear();
            Gesamtgewichtung.AddRange(gesamtgewichtung.Stories);
        }
    }
}
