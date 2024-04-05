using AudioControl.Intefaces;
using AudioDeviceManager.DllImport.Enums;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
