using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl
{
	[StructLayout(LayoutKind.Sequential)]
	public struct AUDIO_VOLUME_NOTIFICATION_DATA
	{
		public Guid guidEventContext;
		public bool bMuted;
		public float fMasterVolume;
		public uint nChannels;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public float[] afChannelVolumes;
	}
}
