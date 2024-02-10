using System.Windows;
using Forms = System.Windows.Forms;

namespace AudioControl.WpfUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SetupNotifyIcon();
            this.MainWindow = new MainWindow();
            this.MainWindow.Show();
            base.OnStartup(e);
        }

        private void SetupNotifyIcon()
        {
            Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("Assets/pci-card-sound.ico");
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
        }

        private void NotifyIcon_DoubleClick(object? sender, System.EventArgs e)
        {
            if(this.MainWindow != null && !this.MainWindow.IsVisible)
            {
                this.MainWindow.Show();
            }
        }
    }
}
