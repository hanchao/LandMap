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
    public partial class PropertyForm : Form
    {
        public PropertyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新界面
        /// </summary>
        /// <param name="objRd">记录集</param>
        public void initData(SuperMapLib.soRecordset objRd)
        {                                             
            String[] value = new String[2];
            for (int i = 1; i <= objRd.FieldCount; i++)
            {
                value[0] = objRd.GetFieldInfo(i).Name; //得到属性名                                                                  
                value[1] = objRd.GetFieldValue(i).ToString(); //得到属性值    
                this.dataGridViewX1.Rows.Add(value);
            }
        }

        /// <summary>
        /// 更新界面
        /// </summary>
        /// <param name="objSelection">选择集</param>
        public void initData(SuperMapLib.soSelection objSelection){
            if (objSelection == null)
            {
                return;
            }

            this.dataGridViewX1.Rows.Clear();

            SuperMapLib.soRecordset objRd;

            objRd = objSelection.ToRecordset(false); //提取所选对象的属性数据                                                  

            initData(objRd);
            
            objRd = null;  
        }
    }
}
