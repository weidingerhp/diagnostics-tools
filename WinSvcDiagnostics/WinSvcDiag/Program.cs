using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using Microsoft.Win32;

namespace LowLevelDesign.Diagnostics
{
    static class Program
    {
        static void Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.Any()) {
                return;
            }
            if (result.Value.ShouldInstall && result.Value.ShouldUninstall) {
                Console.WriteLine("You need to decide whether you would like to install or uninstall a hook.");
                return;
            }

            if (result.Value.ListHooks) {
                ListRegistryEntries();
                return;
            }

            if (result.Value.Timeout > 0) {
                if (!IsUserAdmin()) {
                    Console.WriteLine("You must be an administrator to set a service wait time. Run the application from the administrative command line.");
                    return;
                }
                SetServiceTimeout(result.Value.Timeout);
            } else {
                if (GetServiceTimeout() < 60000) { 
                    // service will be killed if it does not respond under 1 min.
                    Console.WriteLine("Warning: current ServicePipeTimeout is set under 1 min - this time might not be enough " +
                        "to attach with the debugger to the service and start debugging it. It's highly recommended to set this " +
                        "value to at least 4 min (you may use -timeout option for this purpose)");
                }
            }

            var svcpath = result.Value.ServiceExePath;
            if (svcpath == null) {
                Console.WriteLine("A path to the service exe file must be provided.");
                return;
            }

            if (result.Value.ShouldInstall) {
                SetupRegistry(svcpath, true);
            } else if (result.Value.ShouldUninstall) {
                SetupRegistry(svcpath, false);
            } else {
                // here the logic will be a bit more complicated - we will start the process
                // as suspended and then wait for the debugger to appear
                var s = new Suspender(svcpath);
                s.StartProcessAndWaitForDebugger(result.Value.ServiceArgs);
            }
        }

        private static bool IsUserAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            Debug.Assert(identity != null);
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void SetupRegistry(String appImageExe, bool isInstallation)
        {
            Debug.Assert(appImageExe != null);
            if (!IsUserAdmin()) {
                Console.WriteLine("You must be admin to do that. Run the app from the administrative console.");
                return;
            }
            // extrace image.exe if path is provided
            appImageExe = Path.GetFileName(appImageExe);
            var regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", true);
            Debug.Assert(regkey != null);
            // add to image file execution path
            if (isInstallation) {
                regkey = regkey.CreateSubKey(appImageExe);
                Debug.Assert(regkey != null, "regkey != null");
                regkey.SetValue("Debugger", "\"" + Assembly.GetExecutingAssembly().Location + "\"");
            } else {
                regkey.DeleteSubKey(appImageExe, false);
            }
        }

        private static void ListRegistryEntries()
        {
            var asmpath = "\"" + Assembly.GetExecutingAssembly().Location + "\"";

            var regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", true);
            Debug.Assert(regkey != null);

            var hooks = new List<String>();
            foreach (var skn in regkey.GetSubKeyNames()) {
                var sk = regkey.OpenSubKey(skn, false);
                var v = sk.GetValue("Debugger") as String;
                if (v != null && v.StartsWith(asmpath, StringComparison.OrdinalIgnoreCase)) {
                    hooks.Add(skn);
                }
            }

            if (hooks.Count == 0) {
                Console.WriteLine("No hooks found.");
            } else {
                Console.WriteLine("Hooks installed for:");
                foreach (var h in hooks) {
                    Console.WriteLine(" * " + h);
                }
            }
        }

        private static int GetServiceTimeout()
        {
            using (var regkey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control", true)) {
                return (int)regkey.GetValue("ServicesPipeTimeout");
            }
        }

        private static void SetServiceTimeout(int timeoutInSeconds)
        {
            using (var regkey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control", true)) {
                regkey.SetValue("ServicesPipeTimeout", timeoutInSeconds * 1000);
                Console.WriteLine("Timeout changed, but reboot is required for the option to take an effect.");
            }
        }
    }
}
