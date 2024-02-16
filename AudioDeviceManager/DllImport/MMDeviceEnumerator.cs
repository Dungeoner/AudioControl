using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioDeviceManager.DllImport.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioDeviceManager.DllImport
{
    [ComImport, Guid(Guids.MMDeviceEnumeratorCLSIDString)]
    internal class MMDeviceEnumerator
    {
    }
}
