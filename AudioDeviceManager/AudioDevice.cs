using AudioControl;
using AudioControl.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioDeviceManager
{
    internal class AudioDevice : IAudioDevice
    {
        private readonly IMMDevice _immDevice;
        private IAudioEndpointVolume _audioEndpointVolume;
        internal AudioEndpointVolumeCallback DeviceCallback { get;}
        internal AudioDevice(string name, IMMDevice device) { 
            Name = name;
            _immDevice = device;
            DeviceCallback = new AudioEndpointVolumeCallback();
            ActivateEndpoint();
        }
        public string Name { get; }
        public float Gain {
            get
            {
                var result = _audioEndpointVolume.GetMasterVolumeLevelScalar(out float gainLevel);
                if (result != 0)
                {
                    throw new Exception("Failed to get device state. Error code: " + result);
                }
                else return gainLevel * 100;
            }
        }
        public bool IsMuted
        {
            get
            {
                var result = _audioEndpointVolume.GetMute(out bool isMuted);
                if (result != 0)
                {
                    throw new Exception("Failed to get device state. Error code: " + result);
                }
                else return isMuted;
            }
        }

        public void SetTargetGainForDevice(float gainLevel)
        {
            
            _audioEndpointVolume.SetMasterVolumeLevelScalar(gainLevel, Guid.Empty);
            DeviceCallback.OnNotifyCallback = (notificationData) =>
            {
                if (notificationData.fMasterVolume != gainLevel)
                {
                    _audioEndpointVolume.SetMasterVolumeLevelScalar(gainLevel, Guid.Empty);
                }
            };
            _audioEndpointVolume.RegisterControlChangeNotify(DeviceCallback);
        }

        public void SetMute(bool isMute)
        {
            _audioEndpointVolume.SetMute(isMute, Guid.Empty);
        }

        private void ActivateEndpoint()
        {
            var iAdioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
            _immDevice.Activate(ref iAdioEndpointVolume, 0, IntPtr.Zero, out object endpointVolumeInterface);
            _audioEndpointVolume = (IAudioEndpointVolume)endpointVolumeInterface;
        }
    }
}
