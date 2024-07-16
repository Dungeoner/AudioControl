using System.ComponentModel;
using System.Windows.Media;
using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.MVVM.Models;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class DeviceViewModel : ViewModelBase
    {
        public AudioDeviceModel Device { get; set; }

        private readonly MuteIconProvider _muteIconSource;

        public Geometry Icon
        {
            get => Device.IsMuted ? _muteIconSource.IconMuted : _muteIconSource.IconUnmuted;
        }

        public BaseCommand MuteCommand { get; protected set; }

        public DeviceViewModel(AudioDeviceModel device, MuteIconProvider muteIconSource)
        {
            Device = device;
            _muteIconSource = muteIconSource;
            Initialize();
        }
           
        public override void Initialize()
        {
            Device.PropertyChanged += OnDevicePropertyChanged;
            MuteCommand = new BaseCommand(e =>
            {
                Mute();
            });
        }

        private void OnDevicePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName ==  nameof(Device.IsMuted)) {
                OnPropertyChanged(nameof(Icon));
            }
        }

        protected void Mute()
        {
            Device.IsMuted = !Device.IsMuted;
        }
    }
}
