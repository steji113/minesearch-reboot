using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace MineSearch.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<MainWindow>();
        }

        /// <see cref="UnityBootstrapper.InitializeModules" />
        protected override void InitializeModules()
        {
            base.InitializeModules();
            Application.Current.MainWindow = Container.Resolve<MainWindow>();
            Application.Current.MainWindow.Show();
        }
    }
}
