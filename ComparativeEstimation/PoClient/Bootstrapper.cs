using System.Windows;
using Contracts;
using Microsoft.Practices.Unity;
using PoClient.Views;
using Prism.Unity;

namespace PoClient
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterInstance<ICes>(new DummyProvider());
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
