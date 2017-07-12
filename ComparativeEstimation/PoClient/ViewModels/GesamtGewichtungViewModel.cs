using System;
using System.Collections.ObjectModel;
using System.Timers;
using Contracts;
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

        public GesamtGewichtungViewModel()
        {
            restProvider = App.Resolve<ICes>();
            var timer1 = new Timer();
            timer1.Elapsed += (s, e) =>
            {
                Refresh();
            };
            timer1.Interval = 1000;
            timer1.Start();
        }

        public void Refresh()
        {
            var gesamtgewichtung = restProvider.Gesamtgewichtung;
            if (gesamtgewichtung == null)
            {
                gesamtgewichtung = new GesamtgewichtungDto();
            }


            VotingCounterText = $"{gesamtgewichtung.Gewichtungen} / {gesamtgewichtung.Anmeldungen}";
            Gesamtgewichtung.Clear();
            Gesamtgewichtung.AddRange(gesamtgewichtung.Stories);
        }
    }
}
