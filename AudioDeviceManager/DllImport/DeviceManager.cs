using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.Models;
using System.Runtime.InteropServices;

namespace AudioControl
{
    internal class DeviceManager
	{
		private readonly Dictionary<string, IMMDevice> _deviceCollection;
		private AudioEndpointVolumeCallback DeviceCallback { get; set; }

		[DllImport("ole32.dll")]
		private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, IntPtr pUnkOuter,
			tagCLSCTX dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr ppv);
		public DeviceManager()
		{
			_deviceCollection = new Dictionary<string, IMMDevice>();
		}

		internal IEnumerable<string> ObtainDeviceCollection()
		{
			int result = CoCreateInstance(new Guid(Guids.MMDeviceEnumeratorCLSIDString), IntPtr.Zero, tagCLSCTX.CLSCTX_INPROC_SERVER,
				new Guid(Guids.IMMDeviceEnumeratorIIDString), out IntPtr enumeratorPtr);
			if (result != 0)
			{
				Console.WriteLine("Failed to create device enumerator. Error code: " + result);
				return Enumerable.Empty<string>();
			}
			var enumerator = (IMMDeviceEnumerator)Marshal.GetObjectForIUnknown(enumeratorPtr);
			result = enumerator.EnumAudioEndpoints(EDataFlow.eCapture, 0x00000001, out IMMDeviceCollection deviceCollection);
			if (result != 0)
			{
				Console.WriteLine("Failed to get audio collection. Error code: " + result);
				return Enumerable.Empty<string>();
			}
			result = deviceCollection.GetCount(out int deviceCount);
			for (int i = 0; i < deviceCount; i++)
			{
				deviceCollection.Item(i, out IMMDevice device);
				string deviceName = GetDevicePropertyValue(device, new PROPERTYKEY
				{
					fmtid = new Guid(Guids.DeviceFriendlyNameIIDString),
					pid = (int)EPid.PID_FRIENDLY_NAME
				});
				_deviceCollection.Add(deviceName, device);
			}
			return _deviceCollection.Select(x => x.Key);
		}

		internal bool SetTargetGainForDevice(string deviceName, float gainLevel)
		{
			_deviceCollection.TryGetValue(deviceName, out IMMDevice device);
			if (device == null) return false;
			var iAdioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
			device.Activate(ref iAdioEndpointVolume, 0, IntPtr.Zero, out object endpointVolumeInterface);
			IAudioEndpointVolume endpointVolume = (IAudioEndpointVolume)endpointVolumeInterface;
			endpointVolume.SetMasterVolumeLevelScalar(gainLevel, Guid.Empty);
			RegisterCallBackForDevice(endpointVolume, (notificationData) =>
			{
				if (notificationData.fMasterVolume != gainLevel)
				{
					endpointVolume.SetMasterVolumeLevelScalar(gainLevel, Guid.Empty);
					Console.WriteLine("Gain level changed back");
				}
			});
			_deviceCollection.Clear();
			return true;
		}

		private void RegisterCallBackForDevice(IAudioEndpointVolume endpointVolume, Action<AUDIO_VOLUME_NOTIFICATION_DATA> callBack)
		{
			if(DeviceCallback == null)
			{
				DeviceCallback = new AudioEndpointVolumeCallback();
				DeviceCallback.OnNotifyCallback = callBack;
				endpointVolume.RegisterControlChangeNotify(DeviceCallback);
			} else
			{
				DeviceCallback.OnNotifyCallback = callBack;
			}			
		}

		private string GetDevicePropertyValue(IMMDevice device, PROPERTYKEY propertyKey)
		{
			// Get the property store for the device
			device.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore propertyStore);

			// Get the friendly name property key
			//propertyStore.GetCount(out int propertyCount);
			//PROPERTYKEY friendlyNameKey = new PROPERTYKEY();
			//for (int i = 0; i < propertyCount; i++)
			//{
			//	propertyStore.GetAt(i, out PROPERTYKEY key);
			//	if (key.fmtid == targetKey.fmtid && key.pid == targetKey.pid)
			//	{
			//		friendlyNameKey = key;
			//		break;
			//	}
			//}

			// Get the friendly name value
			propertyStore.GetValue(ref propertyKey, out PROPVARIANT propertyValue);
			return propertyValue.GetValue();
		}
	}
}
