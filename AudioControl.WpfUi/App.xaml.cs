using AudioControl.Intefaces;
using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.Core.Interface;
using AudioControl.WpfUi.MVVM.ViewModel;
using AudioDevice.Utility;
using AudioDeviceManager;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Threading;
using System.Windows;
using Forms = System.Windows.Forms;

namespace AudioControl.WpfUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NotifyIconWrapper _notyfyIconWrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<MainViewModel>());
            container.Register(Component.For<TrayViewModel>());
            container.Register(Component.For<IDeviceProvider>().ImplementedBy<DeviceProvider>());
            container.Register(Component.For<IAudioDeviceManager>().ImplementedBy<DeviceManager>());
            container.Register(Component.For<ISettingsManager>().ImplementedBy<SettingsManager>());

            var mainVm = container.Resolve<MainViewModel>();

            this.MainWindow = new MainWindow();
            this.MainWindow.ResizeMode = ResizeMode.CanMinimize;
            MainWindow.DataContext = mainVm;

            var trayVm = container.Resolve<TrayViewModel>();
            var trayWindow = new TrayWindow();
            trayWindow.DataContext = trayVm;

            SetupNotifyIcon(trayWindow);
            this.MainWindow.Show();
            base.OnStartup(e);
        } 
        private void SetupNotifyIcon(TrayWindow trayWindow)
        {
            _notyfyIconWrapper = new NotifyIconWrapper(trayWindow, this.MainWindow);
        }
    }
}
