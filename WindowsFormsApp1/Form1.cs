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
using System.Timers;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private int i;
        private Process process;

        public Form1()
        {
            InitializeComponent();

            StartAt(Hour: 8, Minute: 48);
            
            //MARK: - Enable strat
        }

        private void StartAt(int Hour, int Minute)
        {
            System.Threading.TimerCallback callback = new TimerCallback(ProcessTimerEvent);

            var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minute, 0);

            if (DateTime.Now < dt)
            {
                var timer = new System.Threading.Timer(callback, null,
                                //other occurrences every 24 hours
                                dt - DateTime.Now, TimeSpan.FromHours(24));
            }

        }

        private void ProcessTimerEvent(object obj)
        {
            LaunchApplication();

            quitApplicationAsync();
        }

        private void LaunchApplication()
        {
            Console.WriteLine("Launching");

            process = System.Diagnostics.Process.Start(@"C:\Program Files (x86)\NinjaTrader 8\bin64\NinjaTrader");

            Console.WriteLine("Waiting to restart");

        }

        private async void quitApplicationAsync()
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
