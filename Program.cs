///Authored By Sashank Rao
///

using System;
using System.Windows.Forms;

namespace GraphingData
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}