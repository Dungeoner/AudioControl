using AudioControl.Intefaces;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioCintrol.UI.Models
{
    public class Device
    {
        private readonly IAudioDevice _device;

        public Device(IAudioDevice device)
        {
            _device = device;
        }

        public string Name => _device.Name;

        public double Gain
        {
            get
            {
                return _device.Gain * 100;
            }
            set
            {
                _device.SetTargetGainForDevice(Convert.ToSingle(value / 100));
            }
        }

        public bool IsMuted => _device.IsMuted;

        public void Mute()
        {
            _device.SetMute(!_device.IsMuted);
        }
    }
}
