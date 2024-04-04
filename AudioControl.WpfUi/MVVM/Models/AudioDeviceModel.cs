using AudioControl.Enum;
using AudioControl.Intefaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.MVVM.Models
{
    public class AudioDeviceModel : INotifyPropertyChanged
    {
        private readonly IAudioDevice _audioDevice;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Id => _audioDevice.Id;
        public string Name => _audioDevice.Name;

        public int Gain
        {
            get
            {
                return Convert.ToInt16(_audioDevice.Gain * 100);
            }
            set
            {
                _audioDevice.SetTargetGainForDevice(Convert.ToSingle(value) / 100);
                OnPropertyChanged(nameof(Gain));
            }
        }

        public bool IsMuted
        {
            get { return _audioDevice.IsMuted; }
            set
            {
                _audioDevice.SetMute(value);
                OnPropertyChanged(nameof(IsMuted));
            }
        }

        public EDataFlow DeviceType { get; }

        public AudioDeviceModel(IAudioDevice audioDevice, EDataFlow deviceType)
        {
            _audioDevice = audioDevice;
            DeviceType = deviceType;
        }
    }
}
