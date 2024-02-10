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
    public class DeviceViewModel : ViewModelBase
    {
        private readonly IAudioDevice _device;

        private readonly ISettingsManager _settingsManager;

        private string _imageSource;

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

        public BaseCommand MuteCommand { get; private set; }

        public BaseCommand SaveCommand { get; private set; }

        public DeviceViewModel(IAudioDevice device, ISettingsManager settingsManager)
        {
            _device = device;
            _settingsManager = settingsManager;
            Initialize();
        }
           
        public override void Initialize()
        {
            MuteCommand = new BaseCommand(e =>
            {
                IsMuted = !_device.IsMuted;
            });
            SaveCommand = new BaseCommand(e =>
            {
                Save();
            });
            LoadSettings();
        }

        //public void Mute()
        //{
        //    _device.SetMute(!_device.IsMuted);
        //    ImageSource = _device.IsMuted ? "//Assets/mic-fill.png" : "//Assets/mic-mute-fill.png";
        //}

        public void Save()
        {
            var settigsModel = new DeviceSettingsModel
            {
                Name = Name,
                Gain = Gain,
                IsMuted = IsMuted
            };
            _settingsManager.SaveSettings(settigsModel);
        }

        private void LoadSettings()
        {
            var settings = _settingsManager.LoadSettings(_device.Name);
            if (settings != null)
            {
                Gain = settings.Gain;
                if (_device.IsMuted != settings.IsMuted)
                {
                    _device.SetMute(settings.IsMuted);
                }
            }
        }
    }
}
