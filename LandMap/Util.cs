using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LandMap
{
    /// <summary>
    /// 工具类
    /// </summary>
    class Util
    {
        /// <summary>
        /// Color转换成Object中的颜色
        /// </summary>
        /// <param name="clr">Color颜色</param>
        /// <returns></returns>
        public static UInt32 ColorToUInt32(Color clr)
        {
             return System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(clr));
        }

        /// <summary>
        /// 格式化距离
        /// </summary>
        /// <param name="dis">距离</param>
        /// <returns>返回带单位的距离</returns>
        public static String formatDistance(Double dis)
        {
            String str = "";
            if (dis > 1000)
            {
                str = String.Format("{0:F}", dis / 1000) + "千米";
            }
            else
            {
                str = String.Format("{0:F}", dis) + "米";
            }
            return str;
        }

        /// <summary>
        /// 格式化面积
        /// </summary>
        /// <param name="area">面积</param>
        /// <returns>返回带单位的面积</returns>
        public static String formatArea(Double area)
        {
            String str = "";
            if (area > 1000000)
            {
                str = String.Format("{0:F}", area / 1000000) + "平方千米";
            }
            else
            {
                str = String.Format("{0:F}", area) + "平方米";
            }
            return str;
        }


        public static Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            Color color = Color.FromArgb(int_Red, int_Green, int_Blue);
            return color;

        }

        //PieItem segment1 = myPane.AddPieSlice(20, Color.Navy, Color.White, 45f, 0, "North");
        //PieItem segment3 = myPane.AddPieSlice(30, Color.Purple, Color.White, 45f, .0, "East");
        //PieItem segment4 = myPane.AddPieSlice(10.21, Color.LimeGreen, Color.White, 45f, 0, "West");
        //PieItem segment2 = myPane.AddPieSlice(40, Color.SandyBrown, Color.White, 45f, 0.2, "South");
        //PieItem segment6 = myPane.AddPieSlice(250, Color.Red, Color.White, 45f, 0, "Europe");
        //PieItem segment7 = myPane.AddPieSlice(50, Color.Blue, Color.White, 45f, 0.2, "Pac Rim");
        //PieItem segment8 = myPane.AddPieSlice(400, Color.Green, Color.White, 45f, 0, "South America");
        //PieItem segment9 = myPane.AddPieSlice(50, Color.Yellow, Color.White, 45f, 0.2, "Africa");

        public static Color[] defaultcolor = new Color[]{Color.Navy,Color.Purple,Color.LimeGreen,Color.SandyBrown,Color.Red,
        Color.Blue,Color.Green,Color.Yellow};

        public static Color[] GetColorTable(int count)
        {
            Color[] color = new Color[count];
            for (int i = 0; i < count; i++)
            {
                if (i < defaultcolor.Length)
                {
                    color[i] = defaultcolor[i];
                }
                else
                {
                    color[i] = GetRandomColor();
                }
            }
            return color;

        }
    }
}
