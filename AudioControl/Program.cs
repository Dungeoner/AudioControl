namespace AudioControl
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var microAdjuster = new MicrophoneGainAdjuster();
			microAdjuster.AdjustMicrophoneGain();
			Console.ReadKey();
		}
	}
}