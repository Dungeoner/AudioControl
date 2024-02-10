using System.Runtime.InteropServices;

namespace AudioControl.Intefaces
{
	[Guid(Guids.IAudioEndpointVolumeCallbackIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioEndpointVolumeCallback
	{
		void OnNotify(AUDIO_VOLUME_NOTIFICATION_DATA notificationData);
	}
}
