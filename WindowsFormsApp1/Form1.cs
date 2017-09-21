using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private int i;
        private Process process;

        public Form1()
        {
            InitializeComponent();

            launchApplication();

            quitApplication();

            //MARK: - TODO Split Start func
            //MARK: - TODO Start time
            //MARK: - TODO End time
        }

        private async void launchApplication()
        {
            Console.WriteLine("Launching");

            process = System.Diagnostics.Process.Start(@"C:\Program Files (x86)\NinjaTrader 8\bin64\NinjaTrader");
            
            Console.WriteLine("Waiting to restart");

        }

        private async void quitApplication()
        {
            await PutTaskDelay();

            Console.WriteLine("Quitting Application");

            process.Kill();
        }
            async Task PutTaskDelay()
        {
       
            for (i = 0; i < 20; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(1000);
            }
        }

   
    }
}
