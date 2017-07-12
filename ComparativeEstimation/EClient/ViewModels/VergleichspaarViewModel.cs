using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Contracts;
using Vergleichspaar = EClient.Models.Vergleichspaar;

namespace EClient.ViewModels
{
    public class VergleichspaarViewModel
    {
        private readonly ICes ces;

        public VergleichspaarViewModel(ICes ces)
        {
            this.ces = ces;

            this.OkCommand = new Command(this.Submit);

            Lade_Vergleichspaare();
        }

        public ObservableCollection<Vergleichspaar> Vergleichspaare { get; set; }

        public ICommand OkCommand { get; set; }

        internal void Lade_Vergleichspaare()
        {
            this.Vergleichspaare = new ObservableCollection<Vergleichspaar>(this.ces.Vergleichspaare.Select(Mappings.MapVergleichspaar));
        }

        private void Submit()
        {
            var votings = this.Vergleichspaare.Select(Mappings.MapGewichtetesVergleichspaar);
            this.ces.Gewichtung_regischtriere(votings, Registrieung_erfolgreich, Registrieung_fehlgeschlagen);
        }

        private void Registrieung_erfolgreich()
        {
            Application.Current.Shutdown();
        }

        private void Registrieung_fehlgeschlagen()
        {
            MessageBox.Show("Die Gewichtung ist inkonsistent");
        }
    }
}
