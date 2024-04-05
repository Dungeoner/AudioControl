using AudioControl.Models;
using AudioDeviceManager.DllImport.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AudioDeviceManager.DllImport.Interfaces
{
    [Guid(Guids.IMMNotificationClientIIDString)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMNotificationClient
    {
        [PreserveSig]
        void OnDeviceStateChanged([In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, DeviceState dwNewState);
        [PreserveSig]
        void OnDeviceAdded([In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);
        [PreserveSig]
        void OnDeviceRemoved([In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);
        [PreserveSig]
        void OnDefaultDeviceChanged(EDataFlow flow, ERole role, [In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);
        [PreserveSig]
        void OnPropertyValueChanged([In, MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, PropertyKey key);
    }
}
