namespace AudioControl
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var deviceManager = new DeviceManager();
			var settingsManager = new SettingsManager();
			if (settingsManager.IsSavedExists())
			{
				var settings = settingsManager.ReadSettings();
				if (ApplySettigs(settings, deviceManager))
				{
					Console.WriteLine($"Device {settings.DeviceName} set to {settings.GainLevel}");
				}
				else
				{
					Console.WriteLine("Can't load settings");
				}
			} else
			{
				SelectDevice(deviceManager, settingsManager);
			}
			bool running = true;
			do
			{
				string cmd = Console.ReadLine();
				switch (cmd.ToLower())
				{
					case "select" : SelectDevice(deviceManager, settingsManager);
						break;
					case "exit" : running= false;
						break;
					case "help":
						Console.WriteLine("'select for select device'");
						Console.WriteLine("'exit for stop app'");
						break;
				}
			}
			while (running);
		}

		private static bool ApplySettigs(SettingsModel settings, DeviceManager deviceManager)
		{
			var deviceCollection = deviceManager.ObtainDeviceCollection();
			if(deviceCollection.Contains(settings.DeviceName))
			{
				return deviceManager.SetTargetGainForDevice(settings.DeviceName, settings.GainLevel);
			} else
			{
				return false;
			}
			
		}

		private static bool SelectDevice(DeviceManager deviceManager, SettingsManager settingsManager)
		{
			var deviceCollection = deviceManager.ObtainDeviceCollection();
			Console.WriteLine("Select device:");
			for (int i = 0; i < deviceCollection.Count(); i++)
			{
				Console.WriteLine((i + 1) + ". " + deviceCollection.ElementAt(i));
			}
			var cmd = Console.ReadLine();
			var isCorrect = int.TryParse(cmd, out int deviceNumber);
			if (isCorrect)
			{
				Console.WriteLine("Set gain level from 1 to 100:");
				cmd = Console.ReadLine();
				isCorrect = float.TryParse("0." + cmd, out float gain);
				if (isCorrect)
				{
					var deviceName = deviceCollection.ElementAt(deviceNumber - 1);
					var isSucsesfullySet = deviceManager.SetTargetGainForDevice(deviceName, gain);
					if (isSucsesfullySet)
					{
						Console.WriteLine($"{deviceName} selected sucsesfully");
						settingsManager.WriteSettings(new SettingsModel()
						{
							DeviceName = deviceName,
							GainLevel = gain
						});
						return true;
					}
					else
					{
						Console.WriteLine("Can't select device");
						return false;
					}
				}
			}
			Console.WriteLine("Wrong console input");
			return false;
		}
	}
}