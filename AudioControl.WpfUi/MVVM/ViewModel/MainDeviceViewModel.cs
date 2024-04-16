using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.MVVM.Models;
using AudioDevice.Utility;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    internal class MainDeviceViewModel : DeviceViewModel
    {
        public SaveCommand SaveCommand { get; }

        public MainDeviceViewModel(AudioDeviceModel device, SaveCommand saveCommand, MuteIconProvider muteIconSource) :
            base(device, muteIconSource)
        {
            SaveCommand = saveCommand;
        }
    }
}
