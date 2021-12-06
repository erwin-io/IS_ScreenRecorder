using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IS_ScreenRecorder.Presentation;
using Microsoft.Owin.Hosting;
using IS_ScreenRecorder.App_Start;

namespace IS_ScreenRecorder
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (WebApp.Start<Startup>("http://localhost:8080"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
        }
    }
}
