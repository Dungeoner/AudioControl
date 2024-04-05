using System.Data;
using System.Runtime.InteropServices;
using AudioControl.Intefaces;
using AudioDeviceManager.DllImport.Enums;

namespace AudioDeviceManager.DllImport.Interfaces
{
    [Guid(Guids.IMMDeviceEnumeratorIIDString)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceEnumerator
    {
        [PreserveSig]
        int EnumAudioEndpoints(EDataFlow dataFlow, DeviceState StateMask, out IMMDeviceCollection device);
        [PreserveSig]
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppEndpoint);
        [PreserveSig]
        int GetDevice(string pwstrId, out IMMDevice ppDevice);
        [PreserveSig]
        int RegisterEndpointNotificationCallback(IMMNotificationClient pClient);
        [PreserveSig]
        int UnregisterEndpointNotificationCallback(IMMNotificationClient pClient);
    }
}
