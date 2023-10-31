using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl.Intefaces
{
    internal interface IAudioDevice
    {
        string Name { get; }
        int Gain { get; set; }
    }
}
