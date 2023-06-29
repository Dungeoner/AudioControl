using AudioControl.Intefaces;
using System.Runtime.InteropServices;

namespace AudioControl
{
	public class MicrophoneGainAdjuster
	{
		[DllImport("ole32.dll")]
		private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, IntPtr pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr ppv);

		[DllImport("ole32.dll")]
		private static extern void CoTaskMemFree(IntPtr pv);
		public void AdjustMicrophoneGain()
		{
			IntPtr enumeratorPtr = IntPtr.Zero;
			IntPtr endpointPtr = IntPtr.Zero;
			IntPtr collectionPtr = IntPtr.Zero;
			try
			{
				int result = CoCreateInstance(new Guid(Guids.MMDeviceEnumeratorCLSIDString), IntPtr.Zero, 1, new Guid(Guids.IMMDeviceEnumeratorIIDString), out enumeratorPtr);
				if (result != 0)
				{
					Console.WriteLine("Failed to create device enumerator. Error code: " + result);
					return;
				}
				IMMDeviceEnumerator enumerator = (IMMDeviceEnumerator)Marshal.GetObjectForIUnknown(enumeratorPtr);
				result = enumerator.EnumAudioEndpoints(EDataFlow.eCapture, 0x00000001, out IMMDeviceCollection deviceCollection);
				if (result != 0)
				{
					Console.WriteLine("Failed to get audio collection. Error code: " + result);
					return;
				}
				result = deviceCollection.GetCount(out int deviceCount);
				for (int i = 0; i < deviceCount; i++)
				{
					// Get the device at the specified index
					deviceCollection.Item(i, out IMMDevice device);
					var iAdioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
					// Activate the device and obtain the corresponding interface
					device.Activate(ref iAdioEndpointVolume, 0, IntPtr.Zero, out object endpointVolumeInterface);

					// Cast the interface to IAudioEndpointVolume and use it as needed
					IAudioEndpointVolume endpointVolume = (IAudioEndpointVolume)endpointVolumeInterface;


					var deviceCallback = new AudioEndpointVolumeCallback();
					deviceCallback.OnNotifyCallback += () =>
					{
						endpointVolume.GetMasterVolumeLevelScalar(out float level);
						if (level != 0.79f)
						{
							endpointVolume.SetMasterVolumeLevelScalar(0.79f, Guid.Empty);
						}
					};
					endpointVolume.RegisterControlChangeNotify(deviceCallback);
					// Get the friendly name of the device
					//endpointVolume.GetVolumeRange(out float minVolume, out float maxVolume);
					string deviceName = GetDeviceFriendlyName(device);

				}
				//IAudioEndpointVolume endpointVolume = (IAudioEndpointVolume)Marshal.GetObjectForIUnknown(endpointPtr);
				//endpointVolume.SetMasterVolumeLevelScalar(gain, Guid.Empty);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to adjust microphone gain: " + ex.Message);
			}
			finally
			{
				if (enumeratorPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(enumeratorPtr);
					//Marshal.ReleaseComObject(enumeratorPtr as object);
					//CoTaskMemFree(enumeratorPtr);
				}

				if (endpointPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(endpointPtr);
					//Marshal.ReleaseComObject(endpointPtr as object);
					//CoTaskMemFree(endpointPtr);
				}
			}

		}
		static string GetDeviceFriendlyName(IMMDevice device)
		{
			// Get the property store for the device
			device.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore propertyStore);

			// Get the friendly name property key
			propertyStore.GetCount(out int propertyCount);
			PROPERTYKEY friendlyNameKey = new PROPERTYKEY();
			for (int i = 0; i < propertyCount; i++)
			{
				propertyStore.GetAt(i, out PROPERTYKEY key);
				if (key.fmtid == PKEY_Device.FMTID && key.pid == PKEY_Device.PID_FRIENDLY_NAME)
				{
					friendlyNameKey = key;
					break;
				}
			}

			// Get the friendly name value
			propertyStore.GetValue(ref friendlyNameKey, out PROPVARIANT friendlyNameValue);
			string friendlyName = friendlyNameValue.GetValue();

			return friendlyName;
		}
	}
}