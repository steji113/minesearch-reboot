using System.Windows;
using Microsoft.Practices.Unity;
using MineSearch.Common;
using MineSearch.Wpf.Views.Windows;
using Prism.Unity;

namespace MineSearch.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        /// <see cref="UnityBootstrapper.InitializeModules" />
        protected override void InitializeModules()
        {
            base.InitializeModules();
            Application.Current.MainWindow = Container.Resolve<MainWindow>();
            Application.Current.MainWindow.Show();
        }

        /// <see cref="UnityBootstrapper.ConfigureContainer" />
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<MainWindow>();
            Container.RegisterType<IPointGenerator, RandomPointGenerator>();
        }
    }
}
