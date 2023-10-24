using System.Runtime.InteropServices;
using AudioControl.Models;

namespace AudioControl.Intefaces
{
    [Guid(Guids.IPropertyStoreIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IPropertyStore
	{
		internal int GetCount(out int propertyCount);
		internal int GetAt(int propertyIndex, out PROPERTYKEY propertyKey);
		internal int GetValue(ref PROPERTYKEY propertyKey, out PROPVARIANT propertyValue);
	}
}
