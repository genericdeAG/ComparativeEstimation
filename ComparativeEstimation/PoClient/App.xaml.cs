using System.Windows;
using Contracts;
using Microsoft.Practices.Unity;

namespace PoClient
{
    public partial class App : Application
    {
        private static readonly UnityContainer Container = new UnityContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            Container.RegisterInstance<ICes>(new DummyProvider());
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
