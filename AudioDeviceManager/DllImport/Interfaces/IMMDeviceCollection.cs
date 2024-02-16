using System.Runtime.InteropServices;

namespace AudioControl.Intefaces
{
	[Guid(Guids.IMMDeviceCollectionIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceCollection
	{
		internal int GetCount(out int count);
        internal int Item(int index, out IMMDevice device);
	}
}
