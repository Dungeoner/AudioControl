using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.MVVM.ViewModel;
using AudioDevice.Utility;
using AudioDeviceManager;
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
            var deviceManager = new DeviceManager();
            var settingsManager = new SettingsManager();
            var mainVm = new MainViewModel(deviceManager, settingsManager);
            this.MainWindow = new MainWindow();
            this.MainWindow.ResizeMode = ResizeMode.CanMinimize;
            MainWindow.DataContext = mainVm;
            var trayVm = new TrayViewModel(deviceManager);
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
