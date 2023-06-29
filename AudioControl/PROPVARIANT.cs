using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct PROPVARIANT
	{
		public ushort vt;
		public ushort wReserved1;
		public ushort wReserved2;
		public ushort wReserved3;
		public IntPtr pVal;
		public IntPtr pReserved;
		public int length;

		public string GetValue()
		{
			return Marshal.PtrToStringUni(pVal);
		}
	}
}
