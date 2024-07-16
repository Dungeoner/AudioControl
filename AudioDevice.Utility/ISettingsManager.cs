namespace AudioDevice.Utility
{
    public interface ISettingsManager
    {
        bool SaveSettings(DeviceSettingsModel settigsModel);
        DeviceSettingsModel LoadSettings(string DeviceName);
    }
}