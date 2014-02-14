using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.China;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;

namespace LandMap
{
    /// <summary>
    /// Poi信息
    /// </summary>
    class PoiInfo
    {
        /// <summary>
        /// Poi名称
        /// </summary>
        public String name;

        /// <summary>
        /// x坐标
        /// </summary>
        public double x;

        /// <summary>
        /// y坐标
        /// </summary>
        public double y;

        public override string ToString()
        {
            return name;
        }
    }

    /// <summary>
    /// Poi查询结果
    /// </summary>
    class PoiResult
    {
        /// <summary>
        /// 查询命中的结果
        /// </summary>
        Hits hits = null;

        public PoiResult(Hits hits)
        {
            this.hits = hits;
        }

        /// <summary>
        /// 数目
        /// </summary>
        public int Count
        {
            get
            {
                return hits.Length();
            }
        }

        public PoiInfo getPoiInfoAt(int index)
        {
            PoiInfo poiInfo = new PoiInfo();
            poiInfo.name = hits.Doc(index).GetField("Name").StringValue();

            Byte[] locationByte = hits.Doc(index).GetField("Location").BinaryValue();

            poiInfo.x = BitConverter.ToDouble(locationByte, 0);
            poiInfo.y = BitConverter.ToDouble(locationByte, 8);

            return poiInfo;
        }
    }

    /// <summary>
    /// Poi查询工具
    /// </summary>
    class PoiSearch
    {
        /// <summary>
        /// 分词器
        /// </summary>
        Analyzer objCA = new ChineseAnalyzer();

        /// <summary>
        /// poi查询器
        /// </summary>
        IndexSearcher searcher = null;

        public PoiSearch()
        {

        }

        /// <summary>
        /// 是否打开
        /// </summary>
        public Boolean IsOpen
        {
            get
            {
                return searcher != null;
            }
        }

        public Boolean open(String poiIndexPath)
        {

            try
            {
                searcher = new IndexSearcher(poiIndexPath);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public PoiResult search(String key)
        {
            Query query = new QueryParser("Name", objCA).Parse(key);
            Hits hits = searcher.Search(query);
            if (hits == null)
            {
                return null;
            }
            return new PoiResult(hits);
        }

        public void close()
        {
            if (searcher != null)
            {
                searcher.Close();
            }
        }
    }
}
