using System.Runtime.InteropServices;

namespace AudioControl.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct PropertyKey
    {
        public Guid fmtid;
        public int pid;
    }
}
