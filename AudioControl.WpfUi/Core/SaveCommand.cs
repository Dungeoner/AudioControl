using AudioControl.WpfUi.MVVM.Models;
using AudioDevice.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AudioControl.WpfUi.Core
{
    public class SaveCommand : ICommand
    {
        private readonly ISettingsManager _settingsManager;

        public SaveCommand(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var device = parameter as AudioDeviceModel;
                var settigsModel = new DeviceSettingsModel
                {
                    Name = device.Name,
                    Gain = device.Gain,
                    IsMuted = device.IsMuted,
                };
                _settingsManager.SaveSettings(settigsModel);
        }
    }
}
