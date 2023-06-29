using System.Runtime.InteropServices;

namespace AudioControl.Intefaces
{
	[Guid(Guids.IMMDeviceIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IMMDevice
	{
		internal int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
		internal int OpenPropertyStore(EStgmAccess eStgmAccess, out IPropertyStore propertyStore);
	}
}
