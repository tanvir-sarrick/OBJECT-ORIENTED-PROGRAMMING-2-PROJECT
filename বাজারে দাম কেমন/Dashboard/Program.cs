using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Opening());
            //Application.Run(new Farmer_Storage());
            //Application.Run(new Customer_to_Farmer());
            //Application.Run(new Admin_to_Customer());
            //Application.Run(new Admin_to_Farmer());
            Application.Run(new Intro());
        }
    }
}
