using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.WpfUi.Core.Event
{
    public class DeviceRemovedEventArgs
    {
        public string DeviceId { get; }

        public DeviceRemovedEventArgs(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
