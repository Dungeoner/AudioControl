using System.Windows.Media;
using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.MVVM.Models;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public abstract class DeviceViewModel : ViewModelBase
    {
        public AudioDeviceModel Device { get; set; }

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

        public BaseCommand MuteCommand { get; protected set; }

        public DeviceViewModel(AudioDeviceModel device, MuteIconProvider muteIconSource)
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

        protected void Mute()
        {
            Device.IsMuted = !Device.IsMuted;
            Icon = Device.IsMuted ? _muteIconSource.IconMuted : _muteIconSource.IconUnmuted;
        }
    }
}
