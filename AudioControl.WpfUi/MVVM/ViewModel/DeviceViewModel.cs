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
    public class DeviceViewModel : ViewModelBase
    {
        public AudioDeviceModel Device { get;}

        private readonly ISettingsManager _settingsManager;

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
        public BaseCommand SaveCommand { get; private set; }

        public DeviceViewModel(AudioDeviceModel device, ISettingsManager settingsManager)
        {
            Device = device;
            _settingsManager = settingsManager;
            Initialize();
        }
           
        public override void Initialize()
        {
            SaveCommand = new BaseCommand(e =>
            {
                Save();
            });
        }

        public void Save()
        {
            var settigsModel = new DeviceSettingsModel
            {
                Name = Device.Name,
                Gain = Device.Gain,
                IsMuted = Device.IsMuted,
            };
            _settingsManager.SaveSettings(settigsModel);
        }
    }
}
