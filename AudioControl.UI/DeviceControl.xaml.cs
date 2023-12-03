
using AudioCintrol.UI.Models;
using AudioControl.Intefaces;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AudioControl.UI
{
    public sealed partial class DeviceControl : UserControl, INotifyPropertyChanged
    {

        //public static readonly DependencyProperty DeviceProperty = DependencyProperty.Register(
        //    nameof(Device), typeof(Device), typeof(DeviceControl), new PropertyMetadata(null));

        //public Device Device
        //{
        //    get
        //    { return (Device)GetValue(DeviceProperty); }
        //    set { SetValue(DeviceProperty, value); }
        //}
        private Device _device;

        private string _imageSource;

        public Device Device
        {
            get { return _device; }
            set
            {
                _device = value;
                RaisePropertyChanged(nameof(Device));
                ImageSource = Device.IsMuted ? "Assets/mic-mute-fill.svg" : "/Assets/mic-fill.svg";
            }
        }

        public string ImageSource
        {
            get { return _imageSource?? "/Assets/mic-fill.svg"; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        } //Device.IsMuted ? "Assets/mic-mute-fill.svg" : "/Assets/mic-fill.svg";

        public DeviceControl()
        {
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ButtonMute_Click(object sender, RoutedEventArgs e)
        {
            Device.Mute();
            ImageSource = Device.IsMuted ? "Assets/mic-mute-fill.svg" : "/Assets/mic-fill.svg";
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
