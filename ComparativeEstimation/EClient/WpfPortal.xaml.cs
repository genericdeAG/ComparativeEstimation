using System.Windows;
using EClient.Tests;

namespace EClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfPortal : Window
    {
        private readonly VergleichspaarViewModel vm = new VergleichspaarViewModel(new CesDummy());
        public WpfPortal()
        {
            InitializeComponent();
            base.DataContext = vm;
        }
    }
}
