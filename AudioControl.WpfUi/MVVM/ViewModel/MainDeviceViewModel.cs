using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.MVVM.Models;
using AudioDevice.Utility;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public enum SaveResultEnum
    {
        Default,
        Succesful, 
        Unsuccesful
    }

    public class MainDeviceViewModel : DeviceViewModel
    {
        private readonly ISettingsManager _settingsManager;

        private SaveResultEnum _saveResult;

        public BaseCommand SaveCommand { get; }

        public SaveResultEnum SaveResult
        {
            get => _saveResult;
            set
            {
                _saveResult = value;
                OnPropertyChanged(nameof(SaveResult));
            }
        }

        public MainDeviceViewModel(AudioDeviceModel device, MuteIconProvider muteIconSource, ISettingsManager settingsManager) :
            base(device, muteIconSource)
        {
            _settingsManager = settingsManager;
            SaveCommand = new BaseCommand(e => Save());
            
        }

        private void Save()
        {
                var result = _settingsManager.SaveSettings(
                    new DeviceSettingsModel
                    {
                        Name = Device.Name,
                        Gain = Device.Gain,
                        IsMuted = Device.IsMuted,
                    });
                SaveResult = result ? SaveResultEnum.Succesful : SaveResultEnum.Unsuccesful;
                SaveResult = SaveResultEnum.Default;
        }
    }
}
