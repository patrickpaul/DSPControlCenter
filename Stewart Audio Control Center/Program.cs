﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SA_Resources
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new StartupForm());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Exception in SA_Resources.Program.Main]: " + ex.Message);
            }
        }
    }
}
