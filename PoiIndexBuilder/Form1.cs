using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.China;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using SuperMapLib;

namespace PoiIndexBuilder
{
    public partial class Form1 : Form
    {
        Analyzer objCA = new ChineseAnalyzer();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBuildIndex_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.WorkerReportsProgress = true;   
            this.backgroundWorker1.RunWorkerAsync();
            this.buttonQuery.Enabled = false;
            this.buttonBuildIndex.Enabled = false;
        }

        public void testBitConverter()
        {
            double x = 116.0;
            double y = 39.0;
            List<Byte> locationByte = new List<Byte>();
            locationByte.AddRange(BitConverter.GetBytes(x));
            locationByte.AddRange(BitConverter.GetBytes(y));

            double x2 = 0;
            double y2 = 0;
            x2 = BitConverter.ToDouble(locationByte.ToArray(), 0);
            y2 = BitConverter.ToDouble(locationByte.ToArray(), 8);
        }

        public bool buildPoiIndex(String sdbName,String indexPath)
        {

            soDataSource ds = axSuperWorkspace1.OpenDataSource(sdbName, "", seEngineType.sceSDBPlus, true);
            if (ds.Datasets.Count == 0)
            {
                return false;
            }

            soDataset dataset = ds.Datasets[1];
            if (dataset == null)
            {
                return false;
            }

            soDatasetVector objDtv = (soDatasetVector)dataset;

            soRecordset objRd = objDtv.Query("", false, null, "");

            if (objRd == null)
            {
                return false;
            }

            int count = objRd.RecordCount;
            int i = 0;

            IndexWriter writer = new IndexWriter(indexPath, objCA, true);
            objRd.MoveFirst();
            while (!objRd.IsEOF())
            {

                Document doc = new Document();
                String name = objRd.GetFieldValueText("NAME_CHN");
                doc.Add(new Field("Name", name, Field.Store.YES, Field.Index.TOKENIZED));
                double x = (double)objRd.GetFieldValue("X_COORD");
                double y = (double)objRd.GetFieldValue("Y_COORD");
                List<Byte> locationByte = new List<Byte>();
                locationByte.AddRange(BitConverter.GetBytes(x));
                locationByte.AddRange(BitConverter.GetBytes(y));

                doc.Add(new Field("Location", locationByte.ToArray(), Field.Store.YES));
                writer.AddDocument(doc);

                objRd.MoveNext();
                i++;

                this.backgroundWorker1.ReportProgress(i*100 / count); 
                //if (i== 100)
                //{
                //    break;
                //}
            }

            this.backgroundWorker1.ReportProgress(100); 
            writer.Close();
            axSuperWorkspace1.Close();


            return true;
        }


        public void TestBuildIndex()
        {
            // 创建索引
            //Analyzer objCA = new StandardAnalyzer(); //分词器
            Analyzer objCA = new ChineseAnalyzer();

            IndexWriter writer = new IndexWriter(@"F:\Index", objCA, true);
            Document doc = new Document();
            doc.Add(new Field("Text", "哦耶,美丽的姑娘。", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("Text", "北京矿业大学", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("Text", "北京矿业大学东门", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("Text", "北京矿业大学图书馆", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("Text", "北京大学", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("Text", "麦当劳", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("Text", "清华大学", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);


            doc = new Document();
            doc.Add(new Field("Text", "沙县小吃", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("Text", "穆图科技有限公司", Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);

            writer.Close();
        }

        public void TestQuery()
        {
            //查询
            //Analyzer objCA = new StandardAnalyzer();
            Analyzer objCA = new ChineseAnalyzer();

            String words = "北京";

            IndexSearcher searcher = new IndexSearcher(@"F:\Index");
            Query query = new QueryParser("Text", objCA).Parse(words);
            Hits hits = searcher.Search(query);
            String msg = "";
            for (int i = 0; i < hits.Length(); i++)
            {
                msg += hits.Doc(i).GetField("Text").StringValue() + "\n";
            }
            MessageBox.Show(msg);
            searcher.Close();
        }

        private void buttonSrc_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = "c://";
            openFileDialog.Filter = "数据源|*.sdb|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxSrc.Text = openFileDialog.FileName;
            }
        }

        private void buttonDes_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dilog = new FolderBrowserDialog();
            dilog.Description = "请选择文件夹";
            if (dilog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxDes.Text = dilog.SelectedPath;
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            String words = this.textBoxKey.Text;

            IndexSearcher searcher = new IndexSearcher(this.textBoxDes.Text);
            Query query = new QueryParser("Name", objCA).Parse(words);
            Hits hits = searcher.Search(query);
            String msg = "";
            for (int i = 0; i < hits.Length(); i++)
            {
                String name = hits.Doc(i).GetField("Name").StringValue();

                Byte[] locationByte = hits.Doc(i).GetField("Location").BinaryValue();
                double x = 0;
                double y = 0;
                x = BitConverter.ToDouble(locationByte, 0);
                y = BitConverter.ToDouble(locationByte, 8);

                msg += String.Format("{0}({1},{2})\n", name, x, y);          
            }
            MessageBox.Show(msg);
            searcher.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            buildPoiIndex(this.textBoxSrc.Text, this.textBoxDes.Text);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.buttonQuery.Enabled = true;
            this.buttonBuildIndex.Enabled = true;
            MessageBox.Show("索引创建完成!");
        }
    }
}
