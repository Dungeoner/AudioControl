using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace AudioControl.WpfUi.Core
{
    internal class NotifyIconWrapper
    {
        private readonly NotifyIcon _notifyIcon;

        private readonly Window _mainWindow;

        private readonly DispatcherTimer _timer;

        public Window TrayWindow { get; }

        public NotifyIconWrapper(Window trayWindow, Window mainWindow)
        {
            if(trayWindow == null)
            {
                throw new ArgumentNullException(nameof(trayWindow));
            }
            TrayWindow = trayWindow;
            TrayWindow.ShowInTaskbar = false;
            TrayWindow.Topmost = true;
            _mainWindow = mainWindow;
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = new System.Drawing.Icon("Assets/pci-card-sound.ico");
            _notifyIcon.Visible = true;
            _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            _notifyIcon.MouseClick += NotifyIcon_Click;
            _timer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher.CurrentDispatcher);
            _timer.Interval = TimeSpan.FromMilliseconds(150);
            _timer.Tick += _timer_Tick;
            _notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            var label = new ToolStripLabel();
            label.Text = "Exit";
            label.Click += Label_Click;
            _notifyIcon.ContextMenuStrip.Items.Add(label);
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            _timer.Stop();
            if (!TrayWindow.IsVisible)
            {
                TrayWindow.Show();
            }
            else
            {
                TrayWindow.Hide();
            }
        }

        private void Label_Click(object? sender, System.EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void NotifyIcon_Click(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            _timer.Start();
        }

        private void NotifyIcon_DoubleClick(object? sender, System.EventArgs e)
        {
            _timer.Stop();
            if (_mainWindow == null)
            {
                return;
            }
            if (!_mainWindow.IsVisible)
            {
                _mainWindow.Topmost = true;
                _mainWindow.Show();
                _mainWindow.Topmost = false;
            }
            else
            {
                _mainWindow.Hide();
            }
        }
    }
}
