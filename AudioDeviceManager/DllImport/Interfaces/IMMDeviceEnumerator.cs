using System.Runtime.InteropServices;
using AudioControl.Enum;

namespace AudioControl.Intefaces
{
    [Guid(Guids.IMMDeviceEnumeratorIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IMMDeviceEnumerator
	{
		internal int EnumAudioEndpoints(EDataFlow dataFlow, uint dwStateMask, out IMMDeviceCollection ppDevices);
		internal int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IntPtr ppEndpoint);
	}
}
