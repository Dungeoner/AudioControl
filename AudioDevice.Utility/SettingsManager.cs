using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AudioDevice.Utility
{
    public class SettingsManager : ISettingsManager
    {
        public DeviceSettingsModel LoadSettings(string deviceName)
        {
            if (File.Exists(StringResources.SettingsFilePath))
            {
                var settingsList = ReadSettingsFile();
                return settingsList.FirstOrDefault(x => x.Name.Equals(deviceName));
            }
            return null;
        }

        public void SaveSettings(DeviceSettingsModel settigsModel)
        {
            var settingsList = ReadSettingsFile();
            var existingSettingModel = settingsList.FirstOrDefault(x => x.Name.Equals(settigsModel.Name));
            if (existingSettingModel != null)
            {
                existingSettingModel.Gain = settigsModel.Gain;
                existingSettingModel.IsMuted = settigsModel.IsMuted;
            }
            else
            {
                settingsList.Add(settigsModel);
            }
            var serializedList = JsonSerializer.Serialize(settingsList);
            using var writer = new StreamWriter(StringResources.SettingsFilePath, false);
            writer.Write(serializedList);
        }

        private List<DeviceSettingsModel> ReadSettingsFile()
        {
            var settingsList = new List<DeviceSettingsModel>();
            using var fileSteream = File.Open(StringResources.SettingsFilePath, FileMode.OpenOrCreate);
            using var reader = new StreamReader(fileSteream);
            var fileContent = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(fileContent))
            {
                try
                {
                    var deserializedContent = JsonSerializer.Deserialize<List<DeviceSettingsModel>>(fileContent);
                    settingsList.AddRange(deserializedContent);

                }
                catch (Exception e)
                {
                }
            }
            return settingsList;
        }
    }
}
