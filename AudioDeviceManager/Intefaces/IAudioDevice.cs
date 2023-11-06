using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.Intefaces
{
    public interface IAudioDevice
    {
        string Name { get; }
        void SetTargetGainForDevice(float gainLevel);
        void SetMute(bool isMute);
    }
}
