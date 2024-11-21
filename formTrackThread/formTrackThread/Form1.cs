using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formTrackThread
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();


        private Thread threadA, threadB, threadC, threadD;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            AllocConsole();
            lblStatus.Text = "-Thread Starts-";


            threadA = new Thread(MyThreadClass.Thread1) { Name = "Thread A", Priority = ThreadPriority.Highest };
            threadB = new Thread(MyThreadClass.Thread1) { Name = "Thread B", Priority = ThreadPriority.Normal };
            threadC = new Thread(MyThreadClass.Thread2) { Name = "Thread C", Priority = ThreadPriority.AboveNormal };
            threadD = new Thread(MyThreadClass.Thread2) { Name = "Thread D", Priority = ThreadPriority.BelowNormal };

            // Start threads
            threadA.Start();
            threadB.Start();
            threadC.Start();
            threadD.Start();


            threadA.Join();
            threadB.Join();
            threadC.Join();
            threadD.Join();


            lblStatus.Text = "-End of Thread-";
        }
        public static class MyThreadClass
        {
            public static void Thread1()
            {
                for (int i = 0; i < 2; i++)
                {
                    Thread thread = Thread.CurrentThread;
                    Console.WriteLine($"Name of Thread: {thread.Name} Process = {i}");
                    Thread.Sleep(500); // Suspend for 0.5 seconds
                }
            }

            public static void Thread2()
            {
                for (int i = 0; i < 6; i++)
                {
                    Thread thread = Thread.CurrentThread;
                    Console.WriteLine($"Name of Thread: {thread.Name} Process = {i}");
                    Thread.Sleep(1500); // Suspend for 1.5 seconds
                }
            }



        }
    }
}