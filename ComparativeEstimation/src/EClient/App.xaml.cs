using System.Windows;
using CeServerProvider;
using Contracts;
using EClient.Properties;
using Microsoft.Practices.Unity;

namespace EClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly UnityContainer unityContainer = new UnityContainer();

        public App()
        {
            this.InitializeComponent();

            this.InitializeRegistrations();
        }

        private void InitializeRegistrations()
        {
            var serverAdresse = Settings.Default.ServerAdresse;
            unityContainer.RegisterInstance<ICes>(new RestProvider(serverAdresse));
        }

        public static T Resolve<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }
}
