namespace AudioControl
{
	public class Program
	{
		private static string _device;
		public static void Main(string[] args)
		{
			var deviceManager = new DeviceManager();
			var deviceCollection = deviceManager.ObtainDeviceCollection();
			Console.WriteLine("Select device:");
			for (int i = 0; i < deviceCollection.Count(); i++)
			{
				Console.WriteLine((i + 1) + ". " + deviceCollection.ElementAt(i));
			}

			string cmd;
			do
			{
				cmd = Console.ReadLine();
				if(string.IsNullOrEmpty(cmd))
				{
					Console.WriteLine("Empty command");
					continue;
				}
				if (string.IsNullOrEmpty(_device))
				{
					var isCorrect = int.TryParse(cmd, out int deviceNumber);
					if (isCorrect)
					{
						var deviceName = deviceCollection.ElementAt(deviceNumber - 1);
						var isSucsesfullySet = deviceManager.SetTargetGainForDevice(deviceName);
						if (isSucsesfullySet)
						{
							Console.WriteLine($"{deviceName} selected sucsesfully");
						}
					}
					else
					{
						Console.WriteLine("Wrong number");
					}
				}
			}
			while (cmd?.ToLower() != "exit");
		}
	}
}