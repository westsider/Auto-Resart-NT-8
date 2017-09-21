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
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private int i;
        private Process process;
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();

            StartAt(Hour: 9, Minute: 45);
            
            //MARK: - Enable strat
            // convert delay to hous, min, sec
            // ui for start time, delay or end time, file location or disc name
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

            makeWindowActive();

            //quitApplicationAsync();
        }

        private  void LaunchApplication()
        {
            Console.WriteLine("Launching");

            process = System.Diagnostics.Process.Start(@"C:\Program Files (x86)\NinjaTrader 8\bin64\NinjaTrader");

        }

        private async void makeWindowActive()
        {
            Console.WriteLine("Waiting to make Active");

            await PutTaskDelay(Seconds: 12);

            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("NinjaTrader");
            if (p.Length > 0)
            {
                SetForegroundWindow(p[0].MainWindowHandle);
                Console.WriteLine("Window is active");
            }
        }


        //private async void quitApplicationAsync()
        //{
        //    await PutTaskDelay(Seconds: 20);

        //    Console.WriteLine("Quitting Application");

        //    process.Kill();
        //}

        async Task PutTaskDelay(int Seconds)
        {
            //int milliSeconds = Seconds * 100;

            for (i = 0; i < Seconds; i++)
            {
                Console.WriteLine(i);

                await Task.Delay(1000);
            }
        }
    } 
}
