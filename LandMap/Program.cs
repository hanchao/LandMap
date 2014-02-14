using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Timers;

namespace LandMap
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           ////此处实例化启动界面
           // StartFrom startform = new StartFrom();
           // startform.ShowDialog();             
           // //此处回归到主界面
            Application.Run(new StartFrom());

        }
        //public static void theout(object source, EventArgs e)
        //{
            
        //}
        //public class myEvent : EventArgs
        //{
        //    StartFrom stform;
        //    public myEvent(StartFrom stf1)
        //    {
        //        stform = stf1;
        //    }
        //}

        //private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        //{
           
        //}
    }  
}
