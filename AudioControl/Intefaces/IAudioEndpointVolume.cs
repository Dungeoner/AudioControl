using System.Runtime.InteropServices;

namespace AudioControl.Intefaces
{
	[Guid(Guids.IAudioEndpointVolumeIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IAudioEndpointVolume
	{
		internal int RegisterControlChangeNotify(IAudioEndpointVolumeCallback callback);
		int UnregisterControlChangeNotify(IntPtr pNotify);
		int GetChannelCount(out uint pnChannelCount);
		int SetMasterVolumeLevel(float fLevelDB, Guid pguidEventContext);
		int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);
		int GetMasterVolumeLevel(out float pfLevelDB);
		int GetMasterVolumeLevelScalar(out float pfLevel);
		int SetChannelVolumeLevel(uint nChannel, float fLevelDB, Guid pguidEventContext);
		int SetChannelVolumeLevelScalar(uint nChannel, float fLevel, Guid pguidEventContext);
		int GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);
		int GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);
		int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, Guid pguidEventContext);
		int GetMute(out bool pbMute);
		int GetVolumeStepInfo(out uint pnStep, out uint pnStepCount);
		int VolumeStepUp(Guid pguidEventContext);
		int VolumeStepDown(Guid pguidEventContext);
		int QueryHardwareSupport(out uint pdwHardwareSupportMask);
		int GetVolumeRange(out float pflVolumeMindB, out float pflVolumeMaxdB, out float pflVolumeIncrementdB);
	}
}
