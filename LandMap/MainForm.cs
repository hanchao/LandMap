using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMapLib;
using DevComponents.DotNetBar;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.China;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
namespace LandMap
{
    public partial class MainForm : Form
    {
        String msgCaption = Properties.Resources.msgCaption;

        String tipPOIKey = Properties.Resources.tipPOIKey;
        String tipDistrictKey = Properties.Resources.tipDistrictKey;

        String allType = "全部";

        /// <summary>
        /// 区域列表
        /// </summary>
        String[] districts = new String[]{ "东城区", "西城区", "朝阳区", "丰台区", "石景山区", "海淀区",
                                 "门头沟区", "房山区", "通州区", "顺义区", "昌平区", "大兴区",
                                 "怀柔区", "平谷区", "密云县", "延庆县" };

        int tudi_curdatasetNameIndex = 0;
        String[] tudi_datasetNames = new String[] { "T25万R","T10万R","T5万R" };
        String[] tudi_TypeFields = new String[] { "XYJDLDM", "EJDLDM", "SJDLDM" };
        int tudi_dataSourceIndex = 1;




        String district_datasetName1 = "区县行政界线R";
        String district_datasetName2 = "乡镇行政界线R";
        String district_datasetName3 = "村行政界线R";
        int district_dataSourceIndex = 1;
        String district_NameField1 = "XZQM";
        String district_NameField2 = "JDMC";
        String district_NameField3 = "JFMC";

        /// <summary>
        /// Poi查询工具
        /// </summary>
        PoiSearch poiSearch = new PoiSearch();

        /// <summary>
        /// 坐标转换器，用于POI坐标（WGS84）转换到本地坐标（地图坐标）。
        /// </summary>
        CoordSysTranslator coordSysTranslator = new CoordSysTranslator();

        /// <summary>
        /// 是否打开地图
        /// </summary>
        Boolean bOpenMap = false;
        double dMinScale = 0.0f;

        /// <summary>
        /// 滑动条对应比例尺
        /// </summary>
        double[] scales = null;
        Boolean bRefreshSlider = false;


        //list 用于保存视图范围
        private List<SuperMapLib.soRect> m_extents = new List<SuperMapLib.soRect>();

        //视图存取list的最大限制数
        private int m_extentRecordsMax = 10;

        //当前显示视图在视图list中的index
        private int m_extentNowIndex = 0;

        //当前视图操作状态 next|last|other
        private String m_viewState = String.Empty;

        /// <summary>
        /// 属性窗体
        /// </summary>
        PropertyForm propertyForm = new PropertyForm();

        /// <summary>
        /// 用于统计地类面。在面之内的对象才进行统计
        /// </summary>
        soGeoRegion searchBoundGeometry = null;

        /// <summary>
        /// 天气信息
        /// </summary>
        String[] weatherInfo = null;

        /// <summary>
        /// 类型列表
        /// </summary>
        TypeCodeTable typeCodeTable = new TypeCodeTable();
        public MainForm()
        {
            InitializeComponent();
            this.textBoxItemSearchKey.TextBox.Text = tipPOIKey;
            this.panelDistrict.Visible = false;
            this.panelRight.Visible = false;
            //this.sideBarLeft.Visible = false;


       //     this.sliderScale.Location = new Point(this.Width - this.sliderScale.Width - 10, this.axSuperMap1.Location.X + 64);

      //      this.buttonRigth.Location = new Point(this.Width - this.buttonRigth.Width - 10, this.axSuperMap1.Location.X + 26);


            //添加区域列表
            foreach (String district in districts)
            {
                this.listViewDistrict.Items.Add(district);
            }

            //设置选择集风格
            this.axSuperMap1.selection.Style.BrushStyle = 0;
            this.axSuperMap1.selection.Style.PenStyle = 0;
            this.axSuperMap1.selection.Style.BrushOpaqueRate = 20;

            //添加类型列表
            this.comboBoxType.Items.Add(allType);
            foreach (TypeCodeNode oneNode in typeCodeTable.AllTypeCodes)
            {
                this.comboBoxType.Items.Add(oneNode);

            }

            //工具按钮状态绑定
            this.buttonItemOpenMap.DataBindings.Add("Checked", this.sideBarLeft, "Visible");

            //test
            //typeCodeTable.getNameByCode(032);
            //test

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 开始后台查询天气
            this.backgroundWorker1.RunWorkerAsync();

            // poi查询器
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            String poiIndexPath = basePath + @"..\..\data\PoiIndex";
            if (!poiSearch.open(poiIndexPath))
            {
                this.toolStripStatusLabelMeasure.Text = "找不到POI索引";
            }
        }

        /// <summary>
        /// 更新工具栏状态
        /// </summary>
        private void refreshToolBar()
        {
            MapPan.Checked = axSuperMap1.Action == seAction.scaPan;
            MapZoomIn.Checked = axSuperMap1.Action == seAction.scaZoomIn;
            MapZoomOut.Checked = axSuperMap1.Action == seAction.scaZoomOut;
            MapZoomFree.Checked = axSuperMap1.Action == seAction.scaZoomFree;
            MeasureArea.Checked = axSuperMap1.Action == seAction.scaTrackPolygon;
            MeasureLength.Checked = axSuperMap1.Action == seAction.scaTrackPolyline;
            buttonItemSelect.Checked = axSuperMap1.Action == seAction.scaSelect;
        }

        /// <summary>
        /// 初始化滑动条
        /// </summary>
        private void initSlider()
        {
            soRect viewBounds = axSuperMap1.ViewBounds;
            axSuperMap1.ViewEntire();
            double minScale = axSuperMap1.ViewScale / 4;
            this.sliderScale.Maximum = 18;
            this.sliderScale.Minimum = 0;
            int count = this.sliderScale.Maximum - this.sliderScale.Minimum + 1;
            scales = new double[count];
            for (int i = 0; i < count; i++)
            {
                scales[i] = minScale;
                minScale *= 2;
            }

            axSuperMap1.ViewBounds = viewBounds;

            axSuperMap1.MinScale = scales[0];
            axSuperMap1.MaxScale = scales[scales.Length - 1];
        }

        /// <summary>
        /// 更新滑动条的位置
        /// </summary>
        private void refreshSlider()
        {
            if (scales == null)
                return;

            double minDiff = Double.MaxValue;
            for (int i = 0; i < scales.Length;i++ )
            {
                double diff = Math.Abs(axSuperMap1.ViewScale - scales[i]);
                if (diff < minDiff)
                {
                    minDiff = diff;  
                }
                else
                {
                    if (this.sliderScale.Value != i-1)
                    {
                        bRefreshSlider = true;
                        this.sliderScale.Value = i-1;
                        bRefreshSlider = false;
                    }   
                    break;
                }
                
            }
        }

        /// <summary>
        /// 控制右侧面板
        /// </summary>
        /// <param name="show">是否显示</param>
        private void showRigthPanel(Boolean show)
        {
            this.panelRight.Visible = show;
            if (this.panelRight.Visible)
            {
                this.buttonRigth.Text = ">>";
                //this.buttonRigth.Location = new Point(this.panelRight.Location.X - this.buttonRigth.Width, this.axSuperMap1.Location.X + 26);
            }
            else
            {
                this.buttonRigth.Text = "<<";
                //this.buttonRigth.Location = new Point(this.Width - this.buttonRigth.Width - 10, this.axSuperMap1.Location.X + 26);
            }
        }

        /// <summary>
        /// 控制右侧面板
        /// </summary>
        /// <param name="show">是否显示</param>
        /// <param name="tabIndex">显示第几个标签</param>
        private void showRigthPanel(Boolean show, int tabIndex)
        {
            showRigthPanel(show);
            this.tabControlRigth.SelectedTabIndex = tabIndex; 
        }

        /// <summary>
        /// 设置地图的显示范围
        /// </summary>
        /// <param name="bounds">需要显示的范围</param>
        private void viewBounds(soRect bounds)
        {
            double width = bounds.Width();
            double heigth = bounds.Width();
            bounds.Left -= width / 2;
            bounds.Right += width / 2;
            bounds.Bottom -= heigth / 2;
            bounds.Top += heigth / 2;

            this.axSuperMap1.ViewBounds = bounds;
            this.axSuperMap1.Refresh();
        }

        private void viewAllPoi(PoiInfo poiInfo)
        {

        }

        /// <summary>
        /// 设置地图的显示范围
        /// </summary>
        /// <param name="objRd">需要显示的记录集</param>
        private void viewRecordset(soRecordset objRd)
        {
            double left = Double.MaxValue;
            double right = Double.MinValue;
            double bottom = Double.MaxValue;
            double top = Double.MinValue;
            objRd.MoveFirst();
            while (!objRd.IsEOF())
            {
                soRect bounds = objRd.GetGeometry().Bounds;
                if (left>bounds.Left)
                {
                    left = bounds.Left;
                }
                if (right < bounds.Right)
                {
                    right = bounds.Right;
                }
                if (bottom > bounds.Bottom)
                {
                    bottom = bounds.Bottom;
                }
                if (top < bounds.Top)
                {
                    top = bounds.Top;
                }
                objRd.MoveNext();
            }

            soRect rect = new soRect();
            rect.Left = left;
            rect.Right = right;
            rect.Bottom = bottom;
            rect.Top = top;

            viewBounds(rect);
        }

        private void recordsetToTrackingLayer(soRecordset objRd)
        {
            soStyle style = new soStyle();
            style.PenColor = Util.ColorToUInt32(Color.Red);
            //style.BrushStyle = 0;
            style.BrushOpaqueRate = 50;

            objRd.MoveFirst();
            while (!objRd.IsEOF())
            {
                soGeometry geo = objRd.GetGeometry();
                this.axSuperMap1.TrackingLayer.AddEvent(geo, style, "Search");
                objRd.MoveNext();
            }
            
        }

        private soRecordset searchBySql(int datasourceIndex, String datasetName, String expression)
        {
            SuperMapLib.soDatasetVector objDtv; //矢量数据集                        
            SuperMapLib.soDataset objDt;                        
            SuperMapLib.soRecordset objRd; //记录集                             

            if (this.axSuperWorkspace1.Datasources.Count == 0)
            {
                MessageBox.Show("找不到数据源！", msgCaption);
                return null;
            }

            SuperMapLib.soDataSource objDS = this.axSuperWorkspace1.Datasources[datasourceIndex];
            if (objDS == null || objDS.Datasets.Count == 0)
            {
                MessageBox.Show("找不到数据集！", msgCaption);
                return null;
            }

            objDt = objDS.Datasets[datasetName];
            if (objDt == null)
            {
                MessageBox.Show("找不到数据集！", msgCaption);
                return null;
            }
            objDtv = (SuperMapLib.soDatasetVector)objDt;
            //使用 SQL 过滤条件从数据集中查询出记录集，SQL 条件的 WHERE 子句部分从编辑框中获取。( Query 方法只适用于 soDatasetVector 类对象)   
            objRd = objDtv.Query(expression, true, null, "");

            if (objRd == null || objRd.RecordCount == 0)
            {
                return null;
            }

            objDt = null;
            objDtv = null;

            return objRd;
        }

        /// <summary>
        /// 查询poi
        /// </summary>
        private void searchpoi()
        {
            String key = this.textBoxItemSearchKey.TextBox.Text.Trim();
            if (key.Length == 0 || key == tipPOIKey)
            {
                MessageBox.Show("请输入搜索关键字！", msgCaption);
                return;
            }

            this.listBoxPoiResult.Items.Clear();
            this.axSuperMap1.TrackingLayer.ClearEvents();

            if (!poiSearch.IsOpen)
            {
                MessageBox.Show("找不到POI索引！", msgCaption);
                return;
            }

            // poi查询
            PoiResult poiResult = poiSearch.search(key);

            if (poiResult == null || poiResult.Count == 0)
            {
                MessageBox.Show("找不到数据！", msgCaption);
                return;
            }

            soStyle style = new soStyle();
            style.PenColor = Util.ColorToUInt32(Color.Blue);
            style.SymbolSize = 30;

            soRect rect = new soRect();

            //获取查询结果
            for (int i = 0; i < Math.Min(poiResult.Count,200); i++)
            {
                PoiInfo poiInfo = poiResult.getPoiInfoAt(i);

                //坐标转换（wgs84 -> 地图坐标）
                coordSysTranslator.convert(ref poiInfo);

                // 加了结果列表
                this.listBoxPoiResult.Items.Add(poiInfo);

                // 加入跟踪层高亮显示
                soGeoPoint geoPoint = new soGeoPoint();
                geoPoint.x = poiInfo.x;
                geoPoint.y = poiInfo.y;
                this.axSuperMap1.TrackingLayer.AddEvent((soGeometry)geoPoint, style, poiInfo.name);

                //计算范围
                if (i==0)
                {
                    //第一个
                    rect.Left = poiInfo.x;
                    rect.Right = poiInfo.x;
                    rect.Bottom = poiInfo.y;
                    rect.Top = poiInfo.y;
                }
                else
                {
                    //向外扩张
                    if (rect.Left > poiInfo.x)
                    {
                        rect.Left = poiInfo.x;
                    }
                    if (rect.Right < poiInfo.x)
                    {
                        rect.Right = poiInfo.x;
                    }
                    if (rect.Bottom > poiInfo.y)
                    {
                        rect.Bottom = poiInfo.y;
                    }
                    if (rect.Top < poiInfo.y)
                    {
                        rect.Top = poiInfo.y;
                    }
                } 
            }

            //viewAllPoi(this.listBoxPoiResult.Items);
            showRigthPanel(true, 1);

            if (poiResult.Count == 1)
            {
                axSuperMap1.CenterX = rect.CenterPoint().x;
                axSuperMap1.CenterY = rect.CenterPoint().y;
            }
            else
            {
                viewBounds(rect);
            }
            
            axSuperMap1.Refresh();
            
        }

        /// <summary>
        /// 查询区域
        /// </summary>
        private Boolean searchDistrict(int datasourceIndex, String datasetName, String fieldName, String key)
        {

            String expression = String.Format("{0} like '*{1}*'", fieldName, key); //sdb的sql通配符是*，不是标准的%


            soRecordset objRd = searchBySql(datasourceIndex, datasetName, expression);
            if (objRd == null)
            {
                return false;
            }

            //刷新地图窗口                                                              
            viewRecordset(objRd);

            //加入跟踪层显示
            recordsetToTrackingLayer(objRd);

            objRd = null;

            return true;
        }

        /// <summary>
        /// 查询统计范围
        /// </summary>
        private void searchBound()
        {
            String key = this.comboBoxQueryKey.Text.Trim();
            Double radius = this.doubleInputRadius.Value;
            if (key.Length == 0 || key == tipDistrictKey)
            {
                MessageBox.Show("请输入搜索关键字！", msgCaption);
                return;
            }

            this.listBoxPoiResult.Items.Clear();
            this.axSuperMap1.TrackingLayer.ClearEvents();

            if (!poiSearch.IsOpen)
            {
                MessageBox.Show("找不到POI索引！", msgCaption);
                return;
            }

            // poi查询
            PoiResult poiResult = poiSearch.search(key);

            if (poiResult == null || poiResult.Count == 0)
            {
                MessageBox.Show("找不到数据！", msgCaption);
            }

            soStyle style = new soStyle();
            style.PenColor = Util.ColorToUInt32(Color.Blue);
            style.SymbolSize = 30;


            //获取第一个查询结果
            PoiInfo poiInfo = poiResult.getPoiInfoAt(0);

            //坐标转换（wgs84 -> 地图坐标）
            coordSysTranslator.convert(ref poiInfo);

            // 加入跟踪层高亮显示
            soGeoPoint geoPoint = new soGeoPoint();
            geoPoint.x = poiInfo.x;
            geoPoint.y = poiInfo.y;
            this.axSuperMap1.TrackingLayer.AddEvent((soGeometry)geoPoint, style, poiInfo.name);

            //计算扩大一圈的面
            searchBoundGeometry = geoPoint.SpatialOperator.Buffer(radius, 25);

            ////将查询结果加入到选择集中，使其高亮显示                              
            //objSelection = this.axSuperMap1.selection;
            //objSelection.FromRecordset(objRd);

            this.axSuperMap1.TrackingLayer.RemoveEvent("SearchBound");
            style = new soStyle();
            style.PenColor = Util.ColorToUInt32(Color.Blue);
            style.BrushStyle = 1;
            this.axSuperMap1.TrackingLayer.AddEvent((soGeometry)searchBoundGeometry, style, "SearchBound");

            //刷新地图窗口                                                              
            viewBounds(searchBoundGeometry.Bounds);

            if (!this.comboBoxQueryKey.Items.Contains(key))
            {
                this.comboBoxQueryKey.Items.Add(key);
            }

        }

        /// <summary>
        /// 查询地块，并统计出来
        /// </summary>
        private void searchdikuai()
        {
            if (this.searchBoundGeometry == null)
            {
                return;
            }

            String dsName = tudi_datasetNames[tudi_curdatasetNameIndex];

            //构造查询条件
            String expression = "";
            Boolean alltype = false;
            if (this.comboBoxType.Text == allType)
            {
                alltype = true;
            }
            else
            {
                TypeCodeNode selectType = (TypeCodeNode)this.comboBoxType.SelectedItem;
                if (selectType != null)
                {
                    expression = String.Format("{0} = {1}", tudi_TypeFields[tudi_curdatasetNameIndex], selectType.code);
                }
            }




            SuperMapLib.soDatasetVector objDtv; //矢量数据集                        
            SuperMapLib.soDataset objDt;                      
            SuperMapLib.soRecordset objRd; //记录集                             

            if (this.axSuperWorkspace1.Datasources.Count == 0)
            {
                MessageBox.Show("找不到数据源！", msgCaption);
                return;
            }

            //从工作空间中获取数据源
            SuperMapLib.soDataSource objDS = this.axSuperWorkspace1.Datasources[tudi_dataSourceIndex];
            if (objDS == null || objDS.Datasets.Count == 0)
            {
                MessageBox.Show("找不到数据集！", msgCaption);
                return;
            }

            //从数据源中获取数据集
            objDt = objDS.Datasets[dsName];
            if (objDt == null)
            {
                MessageBox.Show("找不到数据集！", msgCaption);
                return;
            }
            objDtv = (SuperMapLib.soDatasetVector)objDt;
            //使用 SQL 过滤条件从数据集中查询出记录集，SQL 条件的 WHERE 子句部分从编辑框中获取。( Query 方法只适用于 soDatasetVector 类对象)   
            objRd = objDtv.QueryByDistance((soGeometry)searchBoundGeometry, 0, expression);

            if (objRd == null || objRd.RecordCount == 0)
            {
                MessageBox.Show("找不到数据！", msgCaption);
                return;
            }

            //将查询结果加入到选择集中，使其高亮显示                              
            //objSelection = this.axSuperMap1.selection;
            //objSelection.FromRecordset(objRd);
            //刷新地图窗口                                                              
            this.axSuperMap1.Refresh();



            //统计查询的数据
            Dictionary<String, Double> dic = new Dictionary<String, Double>();
            double allArea = 0;

            this.axSuperMap1.TrackingLayer.ClearEvents();

            objRd.MoveFirst();
            while (!objRd.IsEOF())
            {
                soGeometry geo = objRd.GetGeometry();
                if (geo.Type == SuperMapLib.seGeometryType.scgRegion)
                {
                    // 对面进行裁剪，保留相交的部分
                    soGeometry geoIntersection = geo.SpatialOperator.Intersection((soGeometry)searchBoundGeometry);
                    if (geoIntersection != null)
                    {
                        //按分类统计面积
                        String type = objRd.GetFieldValueText(tudi_TypeFields[tudi_curdatasetNameIndex]);//类型
                        //type = type.Substring(0, 2);
                        String typeName = typeCodeTable.getNameByCode(type); 
                        double area = 0; //面积
                        if (objDS.PJCoordSys.Type == sePJCoordSysType.scPCS_LONGITUDE_LATITUDE)
                        {
                            area = ((soGeoRegion)geoIntersection).GetPreciseArea(objDS.PJCoordSys);
                        }
                        else
                        {
                            area = ((soGeoRegion)geoIntersection).Area;
                        }

                        if (dic.ContainsKey(typeName))
                        {
                            dic[typeName] += area;
                        }
                        else
                        {
                            dic.Add(typeName, area);
                        }

                        //统计总面积
                        allArea += area;


                        // 加入到跟踪层，用以高亮显示
                        soStyle style = new soStyle();
                        style.PenColor = Util.ColorToUInt32(Color.Red);
                        //style.BrushStyle = 0;
                        style.BrushOpaqueRate = 50;
                        this.axSuperMap1.TrackingLayer.AddEvent((soGeometry)geoIntersection, style, "SearchDikuai");
                    }
                }

                objRd.MoveNext();
            }

            //显示统计结果
            if (alltype)
            {
                //图标方式
                ChartForm cf = new ChartForm();
                cf.initData(dic);
                cf.ShowDialog(this);
            }
            else
            {
                //对话框方式
                String msg = String.Format("总面积为{0}", Util.formatArea(allArea));
                MessageBox.Show(msg, msgCaption);
            }


        }

        /// <summary>
        /// 关闭地图
        /// </summary>
        private void closeMap()
        {
            if (bOpenMap)
            {
                axSuperMap1.Close();
                axSuperMap1.Disconnect();
                axSuperWorkspace1.Close();
                axSuperMap1.Connect(axSuperWorkspace1.CtlHandle);
                bOpenMap = false;
            }
        }

        /// <summary>
        /// 打开地图
        /// </summary>
        /// <param name="filename"></param>
        public bool openMap(String filename)
        {
            if (filename == null || filename.Length == 0)
            {
                return false;
            }

            closeMap();
            //建立地图窗口与工作空间的联系，用于显示数据 
            axSuperMap1.Connect(axSuperWorkspace1.CtlHandle);

            //打开工作空间
            String password = "";
            if (axSuperWorkspace1.Open(filename, password))
            {
                soMaps objMaps = axSuperWorkspace1.Maps;

                int mapCount = objMaps.Count;
                if (mapCount > 0)
                {
                    //打开第一幅地图
                    String strname = objMaps[1];

                    if (axSuperMap1.OpenMap(strname))
                    {
                        bOpenMap = true;
                        axSuperMap1.Action = seAction.scaPan;
                        refreshToolBar();

                        initSlider();

                        refreshSlider();

                        axSuperMap1.Refresh();

                        //设置坐标系
                        if (axSuperWorkspace1.Datasources.Count > 0)
                        {
                            coordSysTranslator.setLocalCoordSys(axSuperWorkspace1.Datasources[1].PJCoordSys);
                        }
                        


                        this.sideBarLeft.Visible = false;
                        //curScale = axSuperMap1.ViewScale;

                        //axSuperMap1.MaxScale;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("打开地图失败！", msgCaption);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("工作空间没有地图！", msgCaption);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("打开工作空间失败！", msgCaption);
                return false;
            }
        }


        private void buttonItemOpenMap_Click(object sender, EventArgs e)
        {
            this.sideBarLeft.Visible = true;           
        }

        private void MapZoomOut_Click(object sender, EventArgs e)
        {
            axSuperMap1.Action = seAction.scaZoomOut;
            refreshToolBar();
        }

        private void MapZoomIn_Click(object sender, EventArgs e)
        {
            axSuperMap1.Action = seAction.scaZoomIn;
            refreshToolBar();
        }

        private void MapZoomFree_Click(object sender, EventArgs e)
        {
            axSuperMap1.Action = seAction.scaZoomFree;
            refreshToolBar();
        }

        private void MapPan_Click(object sender, EventArgs e)
        {
            axSuperMap1.Action = seAction.scaPan;
            refreshToolBar();
        }

        private void MapFullScreen_Click(object sender, EventArgs e)
        {
            axSuperMap1.ViewEntire();
        }

        private void MeasureLength_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaTrackPolyline;
            refreshToolBar();
        }

        private void MeasureArea_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaTrackPolygon;
            refreshToolBar();
        }

        private void textBoxItemSearchKey_GotFocus(object sender, EventArgs e)
        {
            if (this.textBoxItemSearchKey.TextBox.Text == tipPOIKey)
            {
                this.textBoxItemSearchKey.TextBox.Text = "";
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            axSuperMap1.Close();
            axSuperWorkspace1.Close();
            bOpenMap = false;
            axSuperMap1.Disconnect();

            if (poiSearch != null)
            {
                poiSearch.close();
            }
            
        }

        private void buttonItemSelect_Click(object sender, EventArgs e)
        {
            axSuperMap1.Action = seAction.scaSelect;
            refreshToolBar();
        }

        private void textBoxItemSearchKey_LostFocus(object sender, EventArgs e)
        {
            if (this.textBoxItemSearchKey.TextBox.Text == "")
            {
                this.textBoxItemSearchKey.TextBox.Text = tipPOIKey;
            }
        }

        private void axSuperMap1_Tracking(object sender, AxSuperMapLib._DSuperMapEvents_TrackingEvent e)
        {

            if (this.axSuperMap1.Action == SuperMapLib.seAction.scaTrackPolyline ||
                this.axSuperMap1.Action == SuperMapLib.seAction.scaTrackPolygon)
            {
                //显示量算的距离和面积

                String measureResult = "";


                if (e.dTotalArea > 0)
                {
                    measureResult += "总面积：" + Util.formatArea(e.dTotalArea) + " ";
                }

                if (e.dCurrentLength > 0)
                {
                    measureResult += "本段距离：" + Util.formatDistance(e.dCurrentLength) + " ";
                }
                if (e.dTotalLength > 0)
                {
                    measureResult += "总距离：" + Util.formatDistance(e.dTotalLength) + " ";
                }

                this.toolStripStatusLabelMeasure.Text = measureResult;
            }
        }

        private void axSuperMap1_Tracked(object sender, EventArgs e)
        {
            //if (e.dTotalArea > 0) this.staregionvalue.Text = e.dTotalArea.ToString();
            //if (e.dCurrentLength > 0) this.stadisvalue.Text = e.dCurrentLength.ToString();
            //if (e.dTotalLength > 0) this.statotledisvalue.Text = e.dTotalLength.ToString();
            if (this.axSuperMap1.Action == SuperMapLib.seAction.scaTrackRectangle ||
                this.axSuperMap1.Action == SuperMapLib.seAction.scaTrackCircle)
            {
                //显示绘制的统计范围
                if (this.axSuperMap1.TrackedGeometry.Type == seGeometryType.scgRect)
                {
                    this.searchBoundGeometry = ((soGeoRect)this.axSuperMap1.TrackedGeometry).ConvertToRegion();
                }
                else if (this.axSuperMap1.TrackedGeometry.Type == seGeometryType.scgCircle)
                {
                    this.searchBoundGeometry = ((soGeoCircle)this.axSuperMap1.TrackedGeometry).ConvertToRegion(72);
                }

                
                this.axSuperMap1.TrackingLayer.RemoveEvent("SearchBound");
                soStyle style = new soStyle();
                style.PenColor = Util.ColorToUInt32(Color.Blue);
                style.BrushStyle = 1;
                this.axSuperMap1.TrackingLayer.AddEvent((soGeometry)searchBoundGeometry, style, "SearchBound");
            }
        }

        private void buttonItemDistrict_Click(object sender, EventArgs e)
        {
            this.panelDistrict.Visible = !this.panelDistrict.Visible;

            if (panelDistrict.Visible)
            {
                //ButtonItem button = (ButtonItem)sender;

                panelDistrict.Location = new Point(0, 0);

                // 放在最前面显示
                panelDistrict.BringToFront();
            }
        }

        private void axSuperMap1_ClickEvent(object sender, EventArgs e)
        {
            this.panelDistrict.Visible = false;
        }

        private void sliderScale_ValueChanged(object sender, EventArgs e)
        {
            if (!bOpenMap)
            {
                return;
            }
            if (bRefreshSlider)
            {
                return;
            }
            if (this.sliderScale.Value >= 0 && this.sliderScale.Value < scales.Length)
            {
                this.axSuperMap1.ViewScale = scales[this.sliderScale.Value];
                this.axSuperMap1.Refresh();
            }     
            
        }

        private void axSuperMap1_GeometrySelected(object sender, AxSuperMapLib._DSuperMapEvents_GeometrySelectedEvent e)
        {
            SuperMapLib.soSelection objSelection;

            objSelection = this.axSuperMap1.selection;
            if (objSelection.Count > 0)
            {
                if (propertyForm != null &&  propertyForm.Visible)
                {
                    //更新属性信息
                    propertyForm.initData(objSelection);
                }
            }
            objSelection = null;
              
        }

        private void buttonItemSearch_Click(object sender, EventArgs e)
        {
            searchpoi();
        }

        
        private void ExportMap_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //openFileDialog.InitialDirectory = "c://";
            saveFileDialog.Filter = "png|*.png|jpg|*.jpg|gif|*.gif|bmp|*.bmp";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String ext = System.IO.Path.GetExtension(saveFileDialog.FileName).ToUpper();
                seFileType type = seFileType.scfPNG;
                if (ext == ".PNG")
                {
                    type = seFileType.scfPNG;
                }
                else if (ext == ".JPG")
                {
                    type = seFileType.scfJPG;
                }
                else if (ext == ".GIF")
                {
                    type = seFileType.scfGIF;
                }
                else if (ext == ".BMP")
                {
                    type = seFileType.scfBMP;
                }
                //导出地图图片
                if (!this.axSuperMap1.OutputMapToFile(saveFileDialog.FileName, type))
                {
                    MessageBox.Show("输出地图失败！", "提示");
                }
            }
        }

        private void listViewDistrict_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.panelDistrict.Visible = false;
            
            String disName = e.Item.Text;

            this.axSuperMap1.TrackingLayer.ClearEvents();
            searchDistrict(district_dataSourceIndex, district_datasetName1, district_NameField1, disName);
        }

        private void axSuperMap1_ActionChanged(object sender, AxSuperMapLib._DSuperMapEvents_ActionChangedEvent e)
        {
            refreshToolBar();
        }

        private void axSuperMap1_AfterMapDraw(object sender, AxSuperMapLib._DSuperMapEvents_AfterMapDrawEvent e)
        {
           
             //地图绘制完成后动作
      
            if (this.m_viewState!="last"&& this.m_viewState!="next")
            {
                if (this.m_extents.Count < m_extentRecordsMax)
                {
                    this.m_extents.Add(this.axSuperMap1.ViewBounds);
                    this.m_extentNowIndex = this.m_extents.Count - 1;
                    this.m_viewState = null;
                }
                else
                {
                    this.m_extents.Add(this.axSuperMap1.ViewBounds);
                    this.m_extents.RemoveAt(0);
                    this.m_extentNowIndex = this.m_extents.Count - 1;
                    this.m_viewState = null;
                }
            }
            this.m_viewState = null;

            //同步滚动条
            refreshSlider();

            for (int i = 1; i <= this.axSuperMap1.Layers.Count; i++)
            {
                soLayer layer = this.axSuperMap1.Layers[i];
                if (layer == null || !layer.Visible)
                {
                    continue;
                }

                soDataset ds = layer.Dataset;
                if (ds == null)
                {
                    continue;
                }

                // 比最小比例尺小，不显示
                if (layer.VisibleScaleMin != 0.0 && axSuperMap1.ViewScale < layer.VisibleScaleMin )
                {
                    continue;
                }

                // 比最大比例尺大，不显示
                if (layer.VisibleScaleMax != 0.0 && axSuperMap1.ViewScale > layer.VisibleScaleMax)
                {
                    continue;
                }
               
                
                for (int j = 0; j < this.tudi_datasetNames.Length; j++)
                {
                    if (tudi_datasetNames[j].CompareTo(ds.Name) == 0)
                    {
                        this.tudi_curdatasetNameIndex = j;
                        break;
                    }
                }
                
            }

            this.comboBoxType.Items.Clear();
            TypeCodeNode[] nodes = this.typeCodeTable.getNodeByLevel(tudi_curdatasetNameIndex+1);
            this.comboBoxType.Items.Add(allType);
            this.comboBoxType.Items.AddRange(nodes);
        }

        private void buttonRigth_Click(object sender, EventArgs e)
        {
            showRigthPanel(!this.panelRight.Visible);
            
        }

        private void listViewSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void listViewSearch_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            soRecordset recordset = axSuperMap1.selection.ToRecordset(true);
            if (recordset != null)
            {
                if (recordset.MoveTo(e.ItemIndex))
                {

                    soGeometry geo = recordset.GetGeometry();
                    if (geo != null)
                    {
                        viewBounds(geo.Bounds);
                    }
                }
            }
            
            
        }

        private void textBoxItemSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchpoi();
            }
        }

        private void buttonSelectBound_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaTrackRectangle;
        }

        private void buttonStatistics_Click(object sender, EventArgs e)
        {
            searchdikuai();
            //ChartForm cf = new ChartForm();
            //cf.ShowDialog(this);
        }

        private void axSuperMap1_DblClick(object sender, EventArgs e)
        {
            if (this.axSuperMap1.Action == seAction.scaSelect)
            {
                SuperMapLib.soSelection objSelection;

                objSelection = this.axSuperMap1.selection;
                if (objSelection.Count > 0)
                {
                    if (propertyForm == null || propertyForm.Visible == false)
                    {
                        propertyForm = new PropertyForm();
                    }
                    
                    propertyForm.initData(objSelection);
                    propertyForm.Show(this);
                   
                    
                }
                objSelection = null;
            }
        }

        private void buttonDikuai_Click(object sender, EventArgs e)
        {
            this.showRigthPanel(true,0);
            
        }

        private void buttonDistrict_Click(object sender, EventArgs e)
        {
            String key = this.comboBoxDistrict.Text.Trim();
            if (key.Length == 0 || key == tipDistrictKey)
            {
                MessageBox.Show(tipDistrictKey, msgCaption);
                return;
            }

            this.axSuperMap1.TrackingLayer.ClearEvents();
            if (!searchDistrict(district_dataSourceIndex, district_datasetName1, district_NameField1, key))
            {
                if (!searchDistrict(district_dataSourceIndex, district_datasetName2, district_NameField2, key))
                {
                    searchDistrict(district_dataSourceIndex, district_datasetName3, district_NameField3, key);
                }
            }
            

            if (!this.comboBoxDistrict.Items.Contains(key))
            {
                this.comboBoxDistrict.Items.Add(key);
            }
            this.panelDistrict.Visible = false;
        }

        private void axSuperMap1_MouseDownEvent(object sender, AxSuperMapLib._DSuperMapEvents_MouseDownEvent e)
        {
            this.panelDistrict.Visible = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            WeatherWebService.WeatherWebServiceSoapClient w = new WeatherWebService.WeatherWebServiceSoapClient("WeatherWebServiceSoap");
            //把webservice当做一个类来操作  
            //string[] s = new string[23];//声明string数组存放返回结果  
            string city = "北京";//获得文本框录入的查询城市  
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.buttonItemWeather.Image = Image.FromFile(@"F:\code\LandMap\LandMap\Resources\weather\" + weatherInfo[8] + "");
            if (weatherInfo != null && weatherInfo[8] != "")
            {
                this.buttonItemWeather.Text = weatherInfo[6];
            }
            //buttonItemWeather.s
        }

        private void buttonItemWeather_Click(object sender, EventArgs e)
        {
            WeatherForm wf = new WeatherForm();
            if (weatherInfo != null && weatherInfo[8] != "")
            {
                wf.initData(weatherInfo);
            }
            wf.ShowDialog(this);

        }

        private void buttonQueryBound_Click(object sender, EventArgs e)
        {
            searchBound();
        }

        private void buttonSelectCircleBound_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaTrackCircle;
        }

        private void buttonItemClear_Click(object sender, EventArgs e)
        {
            //清空选择集和跟踪层
            this.axSuperMap1.TrackingLayer.ClearEvents();
            this.axSuperMap1.selection.RemoveAll();
            this.axSuperMap1.Refresh();
            this.searchBoundGeometry = null;
            
        }

        private void buttonItemPrint_Click(object sender, EventArgs e)
        {
            //打印地图
            axSuperMap1.PrintMap();
        }

        private void listViewDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonItemOtherMap_Click(object sender, EventArgs e)
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

 		private void buttonItemopen_Click(object sender, EventArgs e)
        {
            buttonItemOpenMap_Click(sender, e);
        }


        private void buttonItemfile_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonItemexit_Click(object sender, EventArgs e)
        {
            //退出
            this.Close();
        }

        private void buttonItemamp_Click(object sender, EventArgs e)
        {
            axSuperMap1.Action = seAction.scaZoomIn;
            refreshToolBar();
        }

        private void buttonItemnar_Click(object sender, EventArgs e)
        {
            axSuperMap1.Action = seAction.scaZoomOut;
            refreshToolBar();
        }

        private void buttonItemfre_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaZoomFree;  //自由缩放   

        }

        private void buttonItemtra_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaPan;  //漫游 
        }

        private void buttonItemall_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaZoomFree;  //自由缩放
        }

        private void buttonItemsel_Click(object sender, EventArgs e)
        {
            buttonItemSelect_Click(sender, e);
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            buttonRigth_Click(sender, e);
        }

        private void buttonItemdis_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaTrackPolyline;
            refreshToolBar();
        }

        private void buttonItemare_Click(object sender, EventArgs e)
        {
            this.axSuperMap1.Action = SuperMapLib.seAction.scaTrackPolygon;
            refreshToolBar();
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            buttonItemClear_Click(sender, e);
        }
		
        private void buttonItem9_Click(object sender, EventArgs e)
        {
            //前一视图，向前
            this.m_viewState = "last";
            this.buttonItem10.Enabled = true;
            if (this.m_extentNowIndex == 0)
            {
            }
            else if (this.m_extentNowIndex == 1)
            {
                this.m_extentNowIndex = this.m_extentNowIndex - 1;
                this.axSuperMap1.ViewBounds = this.m_extents[this.m_extentNowIndex];
                this.axSuperMap1.Refresh();
               

               
            }

            else
            {
                this.m_extentNowIndex = this.m_extentNowIndex - 1;
                this.axSuperMap1.ViewBounds = this.m_extents[this.m_extentNowIndex];
                this.axSuperMap1.Refresh();

               
            }
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            //后一视图，向后
            this.m_viewState = "next";
            this.buttonItem10.Enabled = true;
            if (this.m_extentNowIndex == this.m_extents.Count - 1)
            {
            }
            else if (this.m_extentNowIndex == this.m_extents.Count - 2)
            {
                this.m_extentNowIndex = this.m_extentNowIndex + 1;
                this.axSuperMap1.ViewBounds = this.m_extents[this.m_extentNowIndex];
                this.axSuperMap1.Refresh();
                this.buttonItem10.Enabled = false;

               
            }
            else
            {
                this.m_extentNowIndex = this.m_extentNowIndex + 1;
                this.axSuperMap1.ViewBounds = this.m_extents[this.m_extentNowIndex];
                this.axSuperMap1.Refresh();

              
            }
        }

        private void buttonItemexport_Click(object sender, EventArgs e)
        {
            ExportMap_Click(sender, e);
        }

        private void buttonItempri_Click(object sender, EventArgs e)
        {
            axSuperMap1.PrintMap();
        }

        private void buttonItemback2_Click(object sender, EventArgs e)
        {
            buttonItem9_Click(sender, e);
        }

        private void buttonItemfor_Click(object sender, EventArgs e)
        {
            buttonItem10_Click(sender, e);
        }

        private void buttonItemcle_Click(object sender, EventArgs e)
        {
            buttonItemClear_Click( sender,  e);
        }

        private void buttonItemTuDI_Click(object sender, EventArgs e)
        {
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            String filename = basePath + @"..\..\data\土地利用现状\土地利用现状.smw";
            openMap(filename);

        }

        private void buttonItemWorld_Click(object sender, EventArgs e)
        {
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            String filename = basePath + @"..\..\data\world\world.smw";
            openMap(filename);
        }

        private void buttonItemTudiPre_Click(object sender, EventArgs e)
        {
            string basePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            String filename = basePath + @"..\..\data\tudi\beijing.smw";
            openMap(filename);
        }

        private void listBoxPoiResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.axSuperMap1.TrackingLayer.RemoveEvent("selectPoiInfo");
            PoiInfo poiInfo = (PoiInfo)this.listBoxPoiResult.SelectedItem;
            if (poiInfo != null)
            {
                // 加入跟踪层高亮显示

                soStyle style = new soStyle();
                style.PenColor = Util.ColorToUInt32(Color.Red);
                style.SymbolSize = 30;

                soGeoPoint geoPoint = new soGeoPoint();
                geoPoint.x = poiInfo.x;
                geoPoint.y = poiInfo.y;
                this.axSuperMap1.TrackingLayer.AddEvent((soGeometry)geoPoint, style, "selectPoiInfo");
                this.axSuperMap1.TrackingLayer.Refresh();
            }

        }
    }
}