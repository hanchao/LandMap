using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace LandMap
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();

            
        }

        /// <summary>
        /// 更新界面
        /// </summary>
        /// <param name="dic">图标键值对</param>
        public void initData(Dictionary<String, Double> dic)
        {
            if (dic.Count == 0)
            {
                return;
            }
            GraphPane myPane = zedGraphControl1.GraphPane;

            /*
           

            // Set the titles and axis labels

            myPane.Title.Text = "地类面积统计表";
            myPane.Title.FontSpec.Family = "微软雅黑";
            //myPane.Title.FontSpec.Size = 32;
            myPane.Title.FontSpec.IsAntiAlias = true;
            myPane.Title.FontSpec.IsBold = true;

            myPane.XAxis.Title.Text = "地类";
            myPane.XAxis.Title.FontSpec.Family = "宋体";
            //myPane.XAxis.Title.FontSpec.Size = 16;
            myPane.XAxis.Title.FontSpec.IsAntiAlias = true;
            myPane.YAxis.Title.Text = "面积";
            myPane.YAxis.Title.FontSpec.Family = "宋体";
            //myPane.YAxis.Title.FontSpec.Size = 16;
            myPane.YAxis.Title.FontSpec.IsAntiAlias = true;


            //foreach (KeyValuePair<String, Double> kvp in dic)
            //{
            //    double[] values = new double[1]{kvp.Value};
            //    BarItem myBar = myPane.AddBar(kvp.Key, null, values, Color.Red);
            //    myBar.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red);
            //}
            BarItem myBar = myPane.AddBar(null, null, dic.Values.ToArray(), Color.Red);
            myBar.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red);

            myPane.XAxis.Scale.TextLabels = dic.Keys.ToArray();
            myPane.XAxis.Type = AxisType.Text;
            myPane.XAxis.Scale.IsPreventLabelOverlap = false;

            myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();

            */


            // Set the GraphPane title
            myPane.Title.Text = "地类面积统计表";
            myPane.Title.FontSpec.IsItalic = true;
            myPane.Title.FontSpec.Size = 24f;
            myPane.Title.FontSpec.Family = "微软雅黑";

            //// Fill the pane background with a color gradient
            //myPane.PaneFill = new Fill(Color.White, Color.Goldenrod, 45.0f);
            //// No fill for the axis background
            //myPane.AxisFill.Type = FillType.None;

            // Set the legend to an arbitrary location
            myPane.Legend.IsVisible = false;
            //myPane.Legend.Position = LegendPos.Float;
            //myPane.Legend.Location = new Location(0.95f, 0.15f, CoordType.PaneFraction,
            //                           AlignH.Right, AlignV.Top);
            //myPane.Legend.FontSpec.Size = 10f;
            //myPane.Legend.IsHStack = false;

            Color[] colors = Util.GetColorTable(dic.Count);
            int i = 0;
            // Add some pie slices
            foreach (KeyValuePair<String, Double> kvp in dic)
            {
                String label = kvp.Key + "\n" + Util.formatArea(kvp.Value);
                PieItem segment = myPane.AddPieSlice(kvp.Value, colors[i], Color.White, 45f, 0, label);
                segment.LabelDetail.FontSpec.Family = "宋体";
                segment.LabelDetail.FontSpec.Size = 12f;
                i++;
            }

            // Sum up the pie values                                                                                                             
            CurveList curves = myPane.CurveList;
            double total = 0;
            for (int x = 0; x < curves.Count; x++)
                total += ((PieItem)curves[x]).Value;



            zedGraphControl1.AxisChange();

            String[] colvalue = new String[2];

            foreach (KeyValuePair<String, Double> kvp in dic)
            {
                colvalue[0] = kvp.Key; //得到属性名                                                                  
                colvalue[1] = Util.formatArea(kvp.Value); //得到属性值    
                this.dataGridView1.Rows.Add(colvalue);
            }
        }

        public void initData(SuperMapLib.soRecordset objRd)
        {
            //String[] value = new String[2];
            //for (int i = 1; i <= objRd.FieldCount; i++)
            //{
            //    value[0] = objRd.GetFieldInfo(i).Name; //得到属性名                                                                  
            //    value[1] = objRd.GetFieldValue(i).ToString(); //得到属性值    
            //    this.dataGridViewX1.Rows.Add(value);
            //}
        }
    }
}
