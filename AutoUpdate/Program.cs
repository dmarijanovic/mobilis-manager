using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AutoUpdate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string putanja = Application.StartupPath;
            if (!putanja.EndsWith("\\"))
                putanja += "\\";
            if (Application.ExecutablePath.ToLower().EndsWith("upd.exe"))
            {
                Thread.Sleep(1000);
                if (File.Exists(putanja + "AutoUpdate.exe"))
                    File.Delete(putanja + "AutoUpdate.exe");
                File.Copy(putanja + "autoupdateupd.exe", putanja + "AutoUpdate.exe");
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(putanja + "AutoUpdate.exe");
                if (process != null)
                    Application.Exit();

            }
            else
            {
                try
                {
                    if (File.Exists(putanja + "AutoUpdateUPD.exe"))
                        File.Delete(putanja + "AutoUpdateUPD.exe");
                }
                catch { MessageBox.Show("Nemogu obrisati AutoUpdateUPD.exe, " + Application.ExecutablePath); }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            //if (args.Length == 0)
            //{
            //    MessageBox.Show("Molim pokrenite updater iz samog programa");
            //    Application.Exit();
            //}
        }
    }
}