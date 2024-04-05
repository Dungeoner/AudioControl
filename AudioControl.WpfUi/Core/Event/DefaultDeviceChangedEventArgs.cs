using AudioControl.WpfUi.MVVM.Models;

namespace AudioControl.WpfUi.Core.Event
{
    public class DefaultDeviceChangedEventArgs
    {
        public AudioDeviceModel Device { get;}
        public DefaultDeviceChangedEventArgs(AudioDeviceModel audioDeviceModel)
        {
            Device = audioDeviceModel;
        }
    }
}
