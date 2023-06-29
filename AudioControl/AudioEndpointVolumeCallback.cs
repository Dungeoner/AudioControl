using AudioControl.Intefaces;

namespace AudioControl
{
	internal class AudioEndpointVolumeCallback : IAudioEndpointVolumeCallback
	{
		internal Action OnNotifyCallback { get; set; }
		public void OnNotify()
		{
			OnNotifyCallback.Invoke();
		}
	}
}
