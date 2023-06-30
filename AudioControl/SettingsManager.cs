using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AudioControl
{
	internal class SettingsManager
	{
		private const string SettingsFilePath = "settings.json";
		internal bool IsSavedExists()
		{
			return File.Exists(SettingsFilePath);
		}
		internal SettingsModel ReadSettings()
		{
			var content = File.ReadAllText(SettingsFilePath);
			return JsonSerializer.Deserialize<SettingsModel>(content)?? new SettingsModel();
		}
		internal void WriteSettings(SettingsModel settingsModel)
		{
			string content = JsonSerializer.Serialize(settingsModel);
			File.WriteAllText(SettingsFilePath, content);
		}
	}
}
