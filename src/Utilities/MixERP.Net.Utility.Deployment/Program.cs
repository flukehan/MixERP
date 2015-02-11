using System;
using System.Security.Principal;
using System.Windows.Forms;
using MixERP.Net.Utility.Installer.UI;

namespace MixERP.Net.Utility.Installer
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (IsAdmin())
            {
                Application.Run(new InstallationForm());
            }
            else
            {
                MessageBox.Show(@"Please run this program as an administrator.", @"Access Denied.", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static bool IsAdmin()
        {
            var current = WindowsIdentity.GetCurrent();

            if (current != null)
            {
                WindowsPrincipal principle = new WindowsPrincipal(current);
                return principle.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }

        // ReSharper disable once InconsistentNaming
        public static void alert(string message)
        {
            MessageBox.Show(message);
        }

        // ReSharper disable once InconsistentNaming
        public static void success(string message)
        {
            MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ReSharper disable once InconsistentNaming
        public static void warn(string message)
        {
            MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ReSharper disable once InconsistentNaming
        public static void error(string message)
        {
            MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}