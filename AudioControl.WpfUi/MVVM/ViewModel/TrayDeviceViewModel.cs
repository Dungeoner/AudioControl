using AudioControl.Intefaces;
using AudioDevice.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using AudioControl.WpfUi.Core;
using AudioControl.WpfUi.MVVM.Models;

namespace AudioControl.WpfUi.MVVM.ViewModel
{
    public class TrayDeviceViewModel : DeviceViewModel
    {
        public TrayDeviceViewModel(AudioDeviceModel device, MuteIconProvider muteIconSource) :
            base(device,  muteIconSource)
        {
        }
    }
}
