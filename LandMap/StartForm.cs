using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LandMap
{
    public partial class StartFrom : Form
    {
        public StartFrom()
        {           
            InitializeComponent();
        }

        //public void timer1_Tick(object sender, EventArgs e)
        //{
        //    // 关闭启动界面
        //    this.Visible = false;
        //    timer1.Stop();
        //    this.Close();  
        //}

       
       

        //private void StartFrom_Shown(object sender, EventArgs e)
        //{
        //    // 开始计时器
        //    timer1.Interval = 2000;
        //    timer1.Tick += new EventHandler(timer1_Tick);
        //    timer1.Start();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            String filename = basePath + @"..\..\data\土地利用现状\土地利用现状.smw";
            openMap(filename);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            String filename = basePath + @"..\..\data\tudi\beijing.smw";
            openMap(filename);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            String filename = basePath + @"..\..\data\world\world.smw";
            openMap(filename);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = "c://";
            openFileDialog.Filter = "工作空间|*.smw|工作空间|*.sxw|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openMap(openFileDialog.FileName);
            }
        }

        public void openMap(String filename)
        {
            MainForm mainform = new MainForm();
            if (mainform.openMap(filename))
            {
                mainform.ShowDialog(this);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
