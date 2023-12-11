using AudioControl.UI.Models;
using AudioControl.Intefaces;
using AudioDevice.Utility;
using Castle.Windsor;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AudioControl.UI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public ObservableCollection<Device> Devices;

        public MainWindow()
        {
            
            this.InitializeComponent();
            var deviceManager = Ioc.Default.GetService<IAudioDeviceManager>();
            Devices = new ObservableCollection<Device>(deviceManager.ObtainDeviceCollection().Select(x => new Device(x)));
        }
    }
}
