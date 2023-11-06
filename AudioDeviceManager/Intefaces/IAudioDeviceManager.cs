using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.Intefaces
{
    public interface IAudioDeviceManager
    {
        public IEnumerable<IAudioDevice> ObtainDeviceCollection();
    }
}
