namespace AudioDevice.Utility
{
    public interface ISettingsManager
    {
        void SaveSettings(DeviceSettingsModel settigsModel);
        DeviceSettingsModel LoadSettings(string DeviceName);
    }
}