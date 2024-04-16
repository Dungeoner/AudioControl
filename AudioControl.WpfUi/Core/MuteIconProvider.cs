using AudioDeviceManager.DllImport.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AudioControl.WpfUi.Core
{
    public class MuteIconProvider
    {
        private readonly ComponentResourceKey _iconMutedKey;
        private readonly ComponentResourceKey _iconUnmutedKey;
        private Geometry _iconUnmuted;
        private Geometry _iconMuted;

        public Geometry IconUnmuted => _iconUnmuted;

        public Geometry IconMuted => _iconMuted;

        public MuteIconProvider(EDataFlow eDataFlow)
        {
            if (eDataFlow == EDataFlow.eCapture)
            {
                _iconMuted = (Geometry)App.Current.Resources["MicMutedIcon"];
                _iconUnmuted = (Geometry)App.Current.Resources["MicIcon"];
            } else if(eDataFlow == EDataFlow.eRender)
            {
                _iconMuted = (Geometry)App.Current.Resources["VolMutedIcon"];
                _iconUnmuted = (Geometry)App.Current.Resources["VolIcon"];
            }
        }
    }
}
