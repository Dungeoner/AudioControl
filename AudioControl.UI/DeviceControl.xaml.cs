
using AudioControl.UI;
using AudioControl.UI.Models;
using AudioControl.Intefaces;
using AudioControl.Models;
using AudioDevice.Utility;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AudioControl.UI
{
    public sealed partial class DeviceControl : UserControl, INotifyPropertyChanged
    {
        private Device _device;

        private string _imageSource;

        public ISettingsManager SettingsManager { get; set; }

        public Device Device
        {
            get { return _device; }
            set
            {
                _device = value;
                LoadSettings();
                RaisePropertyChanged(nameof(Device));
                ImageSource = _device.IsMuted ? IconsPath.MicMutedIcon : IconsPath.MicIcon;
            }
        }

        public string ImageSource
        {
            get { return _imageSource?? IconsPath.MicIcon; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        }

        public DeviceControl()
        {
            this.InitializeComponent();
            this.Resources = Resources;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ButtonMute_Click(object sender, RoutedEventArgs e)
        {
            Device.Mute();
            ImageSource = Device.IsMuted ? IconsPath.MicMutedIcon : IconsPath.MicIcon;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var settigsModel = new DeviceSettingsModel
            {
                Name = Device.Name,
                Gain = Device.Gain,
                IsMuted = Device.IsMuted
            };
            Ioc.Default.GetService<ISettingsManager>().SaveSettings(settigsModel);
        }

        private void LoadSettings()
        {
            var settings = Ioc.Default.GetService<ISettingsManager>().LoadSettings(_device.Name);
            if (settings != null)
            {
                _device.Gain = settings.Gain;
                if(_device.IsMuted != settings.IsMuted)
                {
                    _device.Mute();
                }
            }
        }
    }
}
