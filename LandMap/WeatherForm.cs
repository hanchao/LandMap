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
    public partial class WeatherForm : Form
    {
        String[] weatherInfo = null;

        public WeatherForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新界面
        /// </summary>
        /// <param name="weatherInfo">天气信息</param>
        public void initData(String[] weatherInfo)
        {
            if (weatherInfo == null)
            {
                return;
            }
            textBoxCity.Text = weatherInfo[1];

            String picFrom = "a_" + System.IO.Path.GetFileNameWithoutExtension(weatherInfo[8]);
            pictureBoxOneFrom.Image = (Image)Properties.Resources.ResourceManager.GetObject(picFrom);
            String picTo = "a_" + System.IO.Path.GetFileNameWithoutExtension(weatherInfo[9]);
            pictureBoxOneTo.Image = (Image)Properties.Resources.ResourceManager.GetObject(picTo);
            this.labelOne.Text = weatherInfo[6] + " " + weatherInfo[5];
            labelLive.Text = weatherInfo[10].Replace("；", "\n").Replace("今日天气实况：","");

            picFrom = "_" + System.IO.Path.GetFileNameWithoutExtension(weatherInfo[15]);
            pictureBoxTwoFrom.Image = (Image)Properties.Resources.ResourceManager.GetObject(picFrom);
            picTo = "_" + System.IO.Path.GetFileNameWithoutExtension(weatherInfo[16]);
            pictureBoxTwoTo.Image = (Image)Properties.Resources.ResourceManager.GetObject(picTo);
            this.labelTwo.Text = weatherInfo[13] + " " + weatherInfo[12];

            picFrom = "_" + System.IO.Path.GetFileNameWithoutExtension(weatherInfo[20]);
            pictureBoxThreeFrom.Image = (Image)Properties.Resources.ResourceManager.GetObject(picFrom);
            picTo = "_" + System.IO.Path.GetFileNameWithoutExtension(weatherInfo[21]);
            pictureBoxThreeTo.Image = (Image)Properties.Resources.ResourceManager.GetObject(picTo);
            this.labelThree.Text = weatherInfo[18] + " " + weatherInfo[17];
        }

        /// <summary>
        /// 点击查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonQuery_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            this.buttonQuery.Enabled = false;
            //WeatherWebService.WeatherWebServiceSoapClient w = new WeatherWebService.WeatherWebServiceSoapClient("WeatherWebServiceSoap");
            ////把webservice当做一个类来操作  
            //string[] s = new string[23];//声明string数组存放返回结果  
            //string city = this.textBoxCity.Text.Trim();//获得文本框录入的查询城市  
            //s = w.getWeatherbyCityName(city);
            ////以文本框内容为变量实现方法getWeatherbyCityName  
            //if (s[8] == "")
            //{
            //    MessageBox.Show("暂时不支持您查询的城市");
            //}
            //else
            //{
            //    initData(s);

            //}  

        }

        /// <summary>
        /// 后天查询天气
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            WeatherWebService.WeatherWebServiceSoapClient w = new WeatherWebService.WeatherWebServiceSoapClient("WeatherWebServiceSoap");
            //把webservice当做一个类来操作  
            //string[] s = new string[23];//声明string数组存放返回结果  
            string city = this.textBoxCity.Text.Trim();//获得文本框录入的查询城市  
            try
            {
                weatherInfo = w.getWeatherbyCityName(city);

                //以文本框内容为变量实现方法getWeatherbyCityName  
                if (weatherInfo[8] == "")
                {
                    //MessageBox.Show("暂时不支持您查询的城市");
                }
                else
                {

                    //pictureBox1.Image = Image.FromFile(@"d:\image\" + s[8] + "");
                    //this.label4.Text = s[1] + " " + s[6];
                    //textBox2.Text = s[10];
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// 查询天气信息完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (weatherInfo != null && weatherInfo[8] != "")
            {
                initData(weatherInfo);
            }
            else
            {
                MessageBox.Show("暂时不支持您查询的城市");
            }
            
            this.buttonQuery.Enabled = true;
        }
    }
}
