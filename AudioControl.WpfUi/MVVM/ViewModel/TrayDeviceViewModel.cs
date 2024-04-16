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

        public AudioDeviceModel Device{
            get { return _device; }
            set
            {
                _device = value;
                OnPropertyChanged(nameof(Device));
            }
        }

        public BaseCommand MuteCommand { get; private set; }

        public TrayDeviceViewModel(AudioDeviceModel device, MuteIconProvider muteIconSource)
        {
            Device = device;
            _muteIconSource = muteIconSource;
            Icon = Device.IsMuted ? _muteIconSource.IconMuted : _muteIconSource.IconUnmuted;
            Initialize();
        }

        public override void Initialize()
        {
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
    }
}
