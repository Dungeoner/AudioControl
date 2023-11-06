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
        private IAudioEndpointVolume AudioEndpointVolume { get; set; }
        internal AudioEndpointVolumeCallback DeviceCallback { get;}
        internal AudioDevice(string name, IMMDevice device) { 
            Name = name;
            _immDevice = device;
            DeviceCallback = new AudioEndpointVolumeCallback();
        }
        public string Name { get; }
        public int Gain {get;}

        public void SetTargetGainForDevice(float gainLevel)
        {
            var iAdioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
            _immDevice.Activate(ref iAdioEndpointVolume, 0, IntPtr.Zero, out object endpointVolumeInterface);
            AudioEndpointVolume = (IAudioEndpointVolume)endpointVolumeInterface;
            AudioEndpointVolume.SetMasterVolumeLevelScalar(gainLevel, Guid.Empty);
            DeviceCallback.OnNotifyCallback = (notificationData) =>
            {
                if (notificationData.fMasterVolume != gainLevel)
                {
                    AudioEndpointVolume.SetMasterVolumeLevelScalar(gainLevel, Guid.Empty);
                }
            };
            AudioEndpointVolume.RegisterControlChangeNotify(DeviceCallback);
        }

        public void SetMute(bool isMute)
        {
            AudioEndpointVolume.SetMute(isMute, Guid.Empty);
        }
    }
}
