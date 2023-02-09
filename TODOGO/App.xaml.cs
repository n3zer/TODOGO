using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Windows;

namespace TODOGO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            if (principal.IsInRole(WindowsBuiltInRole.Administrator) == false && principal.IsInRole(WindowsBuiltInRole.User) == true)
            {
                ProcessStartInfo objProcessInfo = new ProcessStartInfo();
                objProcessInfo.UseShellExecute = true;
                objProcessInfo.FileName = Assembly.GetEntryAssembly().CodeBase;
                objProcessInfo.UseShellExecute = true;
                objProcessInfo.Verb = "runas";
                try
                {
                    Process proc = Process.Start(objProcessInfo);
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
