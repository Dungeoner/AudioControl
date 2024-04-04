using AudioControl.Enum;
using AudioControl.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioDeviceManager.DllImport.Interfaces
{
    [Guid(Guids.IMMEndpointIIDString)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMEndpoint
    { 
        int GetDataFlow(out EDataFlow eDataFlow);
    }
}
