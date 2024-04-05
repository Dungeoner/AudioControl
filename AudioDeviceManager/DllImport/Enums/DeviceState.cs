namespace AudioDeviceManager.DllImport.Enums
{
    [Flags]
    public enum DeviceState : uint
    {
        Active = 0x00000001,
        Disabled = 0x00000002,
        NotPresent = 0x00000004,
        Unplugged = 0x00000008,
        MaskAll = 0x0000000F
    }
}
