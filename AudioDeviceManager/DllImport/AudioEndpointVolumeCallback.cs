using AudioControl.Intefaces;

namespace AudioControl
{
	internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
	{
		internal Action<AUDIO_VOLUME_NOTIFICATION_DATA> OnNotifyCallback { get; set; }
		public void OnNotify(AUDIO_VOLUME_NOTIFICATION_DATA notificationData)
		{
			OnNotifyCallback.Invoke(notificationData);
		}
	}
}
