using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Threading;
using Contracts;
using Prism.Commands;
using Prism.Mvvm;

namespace PoClient.ViewModels
{
    public class GesamtGewichtungViewModel : BindableBase
    {
        private ICes restProvider;

        private string votingCounterText;

        public string VotingCounterText
        {
            get => votingCounterText;
            set => SetProperty(ref votingCounterText, value);
        }

        public ObservableCollection<string> Gesamtgewichtung { get; } = new ObservableCollection<string>();

        public DelegateCommand RefreshCommand { get; }

        public GesamtGewichtungViewModel(ICes restProvider)
        {
            this.restProvider = restProvider;

            RefreshCommand = new DelegateCommand(Refresh);
        }

        public void Refresh()
        {
            var gesamtgewichtung = restProvider.Gesamtgewichtung ?? new GesamtgewichtungDto
            {
                Stories = Array.Empty<string>()
            };

            VotingCounterText = $"{gesamtgewichtung.Gewichtungen} / {gesamtgewichtung.Anmeldungen}";
            Gesamtgewichtung.Clear();
            Gesamtgewichtung.AddRange(gesamtgewichtung.Stories);
        }
    }
}
