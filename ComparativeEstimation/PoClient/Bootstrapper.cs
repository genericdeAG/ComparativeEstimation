using System.Windows;
using CeServerProvider;
using Contracts;
using Microsoft.Practices.Unity;
using PoClient.Properties;
using PoClient.Views;
using Prism.Unity;

namespace PoClient
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            var url = Settings.Default.ServerUrl;
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Keine URL hinterlegt!", "Fehler");
                Application.Current.Shutdown();
            }

            this.Container.RegisterInstance<ICes>(new RestProvider(url));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
