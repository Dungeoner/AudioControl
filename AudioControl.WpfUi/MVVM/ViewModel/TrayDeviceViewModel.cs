using AudioControl.Intefaces;
using AudioDevice.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using AudioControl.WpfUi.Core;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class TrayDeviceViewModel : ViewModelBase
    {
        private readonly IAudioDevice _device;

        private string _imageSource;

        public string Id => _device.Id;

        public string Name => _device.Name;

        public bool IsMuted
        {
            get { return _device.IsMuted; }
            set
            {
                _device.SetMute(value);
                OnPropertyChanged(nameof(IsMuted));
            }
        }
        public int Gain
        {
            get
            {
                return Convert.ToInt16(_device.Gain * 100);
            }
            set
            {
                _device.SetTargetGainForDevice(Convert.ToSingle(value) / 100);
                OnPropertyChanged(nameof(Gain));
            }
        }

        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public TrayDeviceViewModel(IAudioDevice device)
        {
            _device = device;
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
