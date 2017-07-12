using System.Collections.ObjectModel;
using Contracts;

namespace EClient
{
    public class VergleichspaarViewModel
    {
        public VergleichspaarViewModel()
        {
            Vergleichspaare = new ObservableCollection<Vergleichspaar>
            {
                new Vergleichspaar
                {
                    A = "X",
                    B = "Y",
                    Id = "1"
                },
                new Vergleichspaar
                {
                    A = "B",
                    B = "C",
                    Id = "2"
                }
            };
        }

        public ObservableCollection<Vergleichspaar> Vergleichspaare { get; set; }
    }
}
