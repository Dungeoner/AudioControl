using System.Runtime.InteropServices;
using AudioControl.Models;

namespace AudioControl.Intefaces
{
    [Guid(Guids.IPropertyStoreIIDString)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IPropertyStore
	{
		internal int GetCount(out int propertyCount);
		internal int GetAt(int propertyIndex, out PropertyKey propertyKey);
		internal int GetValue(ref PropertyKey propertyKey, out PropertyValue propertyValue);
	}
}
