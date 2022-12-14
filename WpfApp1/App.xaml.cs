﻿using System.Windows;
using AgentOctal.WpfLib.Services;
using AgentOctal.WpfLib.Services.WindowManager;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var vm = new MainWindowVm();
            ServiceManager.GetService<IWindowManagerService>().DisplayWindowFor(vm);
        }
    }
}