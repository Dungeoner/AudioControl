using System.Runtime.InteropServices;
using AudioControl.Enum;

namespace AudioControl.Intefaces
{
    [Guid(Guids.IMMDeviceIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IMMDevice
	{
		internal int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
        internal int OpenPropertyStore(EStgmAccess eStgmAccess, out IPropertyStore propertyStore);
		internal int GetId([Out, MarshalAs(UnmanagedType.LPWStr)]out string pwstrDeviceId);
	}
}
