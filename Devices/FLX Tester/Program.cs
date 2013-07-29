using System;
using System.Windows.Forms;
using SA_Resources;

namespace FLX_Tester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(PIC_Bridge PIC_Conn)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(PIC_Conn));
        }
    }
}
