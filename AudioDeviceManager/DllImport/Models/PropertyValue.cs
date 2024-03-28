using System.Runtime.InteropServices;

namespace AudioControl.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PropertyValue
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
