using System.Runtime.InteropServices;

namespace AudioControl.Intefaces
{
	[Guid(Guids.IAudioEndpointVolumeCallbackIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioEndpointVolumeCallback
	{
		void OnNotify();
	}
}
