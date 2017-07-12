using System.Windows;

namespace EClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfPortal : Window
    {
        private readonly VergleichspaarViewModel vm = new VergleichspaarViewModel();
        public WpfPortal()
        {
            InitializeComponent();
            base.DataContext = vm;
        }
    }
}
