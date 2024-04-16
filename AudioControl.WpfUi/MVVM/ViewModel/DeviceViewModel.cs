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

        private readonly MuteIconProvider _muteIconSource;

        private Geometry _icon;

        public Geometry Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public BaseCommand SaveCommand { get; private set; }

        public BaseCommand MuteCommand { get; private set; }

        public DeviceViewModel(AudioDeviceModel device, ISettingsManager settingsManager, MuteIconProvider muteIconSource)
        {
            Device = device;
            _settingsManager = settingsManager;
            _muteIconSource = muteIconSource;
            Icon = Device.IsMuted ? _muteIconSource.IconMuted : _muteIconSource.IconUnmuted;
            Initialize();
        }
           
        public override void Initialize()
        {
            SaveCommand = new BaseCommand(e =>
            {
                Save();
            });

            MuteCommand = new BaseCommand(e =>
            {
                Mute();
            });
        }

        private void Mute()
        {
            Device.IsMuted = !Device.IsMuted;
            Icon = Device.IsMuted ? _muteIconSource.IconMuted : _muteIconSource.IconUnmuted;
        }

        private void Save()
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
