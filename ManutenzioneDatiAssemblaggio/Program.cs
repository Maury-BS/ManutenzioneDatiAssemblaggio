using System;
using System.Windows.Forms;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    static class Program
    {
        //public static ucProduzione DatiEstrusione;
        //public static ucFermiMacchina Fermi;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //controllo versione
            bool IsExeChanged;
            Versioning.CheckCurrentVersion(out IsExeChanged);
            if (IsExeChanged)
            {
                //rilancio la versione corretta (questa istanza "muore" così)
                System.Diagnostics.Process.Start(Application.ExecutablePath);
            }
            else
            {
                //start
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
        }
    }
}
