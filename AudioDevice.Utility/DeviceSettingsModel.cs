using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioDevice.Utility
{
    public class DeviceSettingsModel
    {
        public string Name { get; set; }
        public int Gain { get; set; }
        public bool IsMuted { get; set; }
    }
}
