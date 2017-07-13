using System.Windows;
using System.Windows.Input;
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
            var mainWindowViewModel = this.DataContext as MainWindowViewModel;
            if (mainWindowViewModel != null)
            {
                mainWindowViewModel.CloseWindow = this.Close;
            }
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var mainWindowViewModel = this.DataContext as MainWindowViewModel;
                mainWindowViewModel?.AddStoryCommand?.Execute();
            }
        }
    }
}
