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
using AudioControl.WpfUi.MVVM.Models;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class TrayDeviceViewModel : ViewModelBase
    {
        private AudioDeviceModel _device;

        private string _imageSource;

        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public AudioDeviceModel Device{
            get { return _device; }
            set
            {
                _device = value;
                OnPropertyChanged(nameof(Device));
            }
        }

        public TrayDeviceViewModel(AudioDeviceModel device)
        {
            Device = device;
            Initialize();
        }

        public override void Initialize()
        {
        }
    }
}
