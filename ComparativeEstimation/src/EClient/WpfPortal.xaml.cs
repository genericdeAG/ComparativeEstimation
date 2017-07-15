using System.Windows;
using Contracts;
using EClient.ViewModels;

namespace EClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfPortal : Window
    {
        public WpfPortal()
        {
            InitializeComponent();
            base.DataContext = new VergleichspaarViewModel(App.Resolve<ICes>());
        }
    }
}
