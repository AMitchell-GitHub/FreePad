using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FreePad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string text = System.IO.File.ReadAllText(@"C:\SyncFusionLicenseKey.txt");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(text);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
