using System.Windows;
using Contracts;
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
            unityContainer.RegisterInstance<ICes>(new CesDummy());
        }

        public static T Resolve<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }
}
