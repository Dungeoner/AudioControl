using AudioControl.Intefaces;
using Castle.Windsor;
using Microsoft.UI.Xaml;
using AudioDeviceManager;
using Microsoft.UI;
using AudioDevice.Utility;
using Windows.Media.Protection.PlayReady;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AudioControl.UI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Ioc.Default.ConfigureServices
            (new ServiceCollection().AddTransient<ISettingsManager, SettingsManager>().
                AddScoped<IAudioDeviceManager, DeviceManager>().BuildServiceProvider()
            );
            m_window = new MainWindow();
            m_window.Activate();
            m_window.AppWindow.Resize(new Windows.Graphics.SizeInt32(800, 250));
        }

        private Window m_window;
    }
}
