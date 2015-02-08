using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.Runtime;
using Topshelf.Runtime.Windows;

namespace Topshelf.Diagnostics
{
    class WindowsHostEnvironmentWithDebugSupport : HostEnvironment
    {
        private readonly WindowsHostEnvironment wrappedHost = new WindowsHostEnvironment();

        public bool IsRunningAsAService {
            get { return Process.GetCurrentProcess().SessionId == 0; }
        }

        public string CommandLine {
            get { return wrappedHost.CommandLine; }
        }

        public Host CreateServiceHost(HostSettings settings, ServiceHandle serviceHandle) {
            return wrappedHost.CreateServiceHost(settings, serviceHandle);
        }

        public void InstallService(InstallHostSettings settings, Action beforeInstall, Action afterInstall, Action beforeRollback, Action afterRollback) {
            wrappedHost.InstallService(settings, beforeInstall, afterInstall, beforeRollback, afterRollback);
        }

        public bool IsAdministrator {
            get { return wrappedHost.IsAdministrator; }
        }

        public bool IsServiceInstalled(string serviceName) {
            return wrappedHost.IsServiceInstalled(serviceName);
        }

        public bool IsServiceStopped(string serviceName) {
            return wrappedHost.IsServiceStopped(serviceName);
        }

        public bool RunAsAdministrator() {
            return wrappedHost.RunAsAdministrator();
        }

        public void SendServiceCommand(string serviceName, int command) {
            wrappedHost.SendServiceCommand(serviceName, command);
        }

        public void StartService(string serviceName, TimeSpan startTimeOut) {
            wrappedHost.StartService(serviceName, startTimeOut);
        }

        public void StopService(string serviceName, TimeSpan stopTimeOut) {
            wrappedHost.StopService(serviceName, stopTimeOut);
        }

        public void UninstallService(HostSettings settings, Action beforeUninstall, Action afterUninstall) {
            wrappedHost.UninstallService(settings, beforeUninstall, afterUninstall);
        }
    }
}
