using System.Text.Json;
using AudioControl.Models;

namespace AudioControl
{
    internal class SettingsManager
	{
		private string SettingsFolderPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AudioControl");
			}
		}
		private string SettingsFilePath
		{
			get
			{
				return Path.Combine(SettingsFolderPath, "settings.json");
			}
		}
		public SettingsModel CurrentDevice { get; set; }
		internal bool IsSavedExists()
		{
			return File.Exists(SettingsFilePath);
		}
		internal SettingsModel ReadSettings()
		{
			var content = File.ReadAllText(SettingsFilePath);
			var settings = JsonSerializer.Deserialize<SettingsModel>(content);
			if (settings != null) {
				CurrentDevice = settings;
				return settings;
			} else
			{
				return new SettingsModel();
			}
		}
		internal void WriteSettings(SettingsModel settingsModel)
		{
			CurrentDevice = settingsModel;
			string content = JsonSerializer.Serialize(settingsModel);
			if (!Directory.Exists(SettingsFolderPath))
			{
				Directory.CreateDirectory(SettingsFolderPath);
			}			
			File.WriteAllText(SettingsFilePath, content);
		}
	}
}
