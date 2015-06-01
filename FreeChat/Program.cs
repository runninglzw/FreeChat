using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace FreeChat
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
            //Application.Run(new MainForm());

            bool bIsRunning;
            Mutex mutexAPP = new Mutex(false, Assembly.GetExecutingAssembly().FullName, out bIsRunning);
            if (!bIsRunning)
            {
                MessageBox.Show("程序正在运行中，只能运行一个聊天程序！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MainForm freeChat = new MainForm();
                freeChat.Text = System.Environment.UserName;
                freeChat.Name = "MainChat" + freeChat.Text;
                freeChat.MaximizeBox = false;
                Application.Run(freeChat);
            }
        }
    }
}
