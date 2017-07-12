using System.Windows;
using PoClient.ViewModels;

namespace PoClient.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MainWindowViewModel).CloseWindow = this.Close;
        }
    }
}
