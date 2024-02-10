using AudioControl;
using AudioControl.Enum;
using AudioControl.Intefaces;
using AudioControl.Models;
using System.Data;
using System.Runtime.InteropServices;
using System.Linq;

namespace AudioDeviceManager
{
    public class DeviceManager : IAudioDeviceManager
    {
        [DllImport("ole32.dll")]
        private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, IntPtr pUnkOuter,
            tagCLSCTX dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr ppv);
        public DeviceManager()
        {
        }

        public IEnumerable<IAudioDevice> ObtainDeviceCollection()
        {
            int result = CoCreateInstance(new Guid(Guids.MMDeviceEnumeratorCLSIDString), IntPtr.Zero, tagCLSCTX.CLSCTX_INPROC_SERVER,
                new Guid(Guids.IMMDeviceEnumeratorIIDString), out IntPtr enumeratorPtr);
            if (result != 0)
            {
                throw new Exception("Failed to create device enumerator. Error code: " + result);
            }
            var enumerator = (IMMDeviceEnumerator)Marshal.GetObjectForIUnknown(enumeratorPtr);
            result = enumerator.EnumAudioEndpoints(EDataFlow.eCapture, 0x00000001, out IMMDeviceCollection immDeviceCollection);
            if (result != 0)
            {
                throw new Exception("Failed to get audio collection. Error code: " + result);
            }
            result = immDeviceCollection.GetCount(out int deviceCount);

            var deviceCollection = new List<IAudioDevice>();
            for (int i = 0; i < deviceCount; i++)
            {
                immDeviceCollection.Item(i, out IMMDevice immDevice);
                string deviceName = GetDevicePropertyValue(immDevice, new PROPERTYKEY
                {
                    fmtid = new Guid(Guids.DeviceFriendlyNameIIDString),
                    pid = (int)EPid.PID_FRIENDLY_NAME
                });
                deviceCollection.Add(new AudioDevice(deviceName, immDevice));
            }
            return deviceCollection;
        }

        private string GetDevicePropertyValue(IMMDevice device, PROPERTYKEY propertyKey)
        {
            // Get the property store for the device
            device.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore propertyStore);

            // Get the friendly name value
            propertyStore.GetValue(ref propertyKey, out PROPVARIANT propertyValue);
            return propertyValue.GetValue();
        }
    }
}
