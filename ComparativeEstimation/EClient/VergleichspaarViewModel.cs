using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Contracts;

namespace EClient
{
    public class VergleichspaarViewModel
    {
        private readonly ICes ces;

        public VergleichspaarViewModel(ICes ces)
        {
            this.ces = ces;

            this.OkCommand = new Command(this.Ok);

            LadeVergleichspaare();
        }

        public ObservableCollection<Vergleichspaar> Vergleichspaare { get; set; }

        public ICommand OkCommand { get; set; }

        internal void LadeVergleichspaare()
        {
            this.Vergleichspaare = new ObservableCollection<Vergleichspaar>(this.ces.Vergleichspaare.Select(Mappings.MapVergleichspaar));
        }

        private void Ok()
        {
            var votings = this.Vergleichspaare.Select(Mappings.MapGewichtetesVergleichspaar);
            this.ces.Gewichtung_regischtriere(votings, null, null);
        }
    }
}
