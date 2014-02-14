using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperMapLib;
namespace LandMap
{
    /// <summary>
    /// WGS-84 到 GCJ-02 的转换（即 GPS 加偏，中国特色）算法是一个普通青年轻易无法接触到的“公开”的秘密。
    /// 这个算法的代码在互联网上是公开的，详情请使用 Google 搜索 "wgtochina_lb" 。
    /// 参考《地球坐标系 (WGS-84) 到火星坐标系 (GCJ-02) 的转换算法》
    /// http://blog.csdn.net/coolypf/article/details/8686588
    /// </summary>
    class EvilTransform
    {
        const double pi = 3.14159265358979324;

        //
        // Krasovsky 1940
        //
        // a = 6378245.0, 1/f = 298.3
        // b = a * (1 - f)
        // ee = (a^2 - b^2) / a^2;
        const double a = 6378245.0;
        const double ee = 0.00669342162296594323;

        //
        // World Geodetic System ==> Mars Geodetic System
        public static void transform(double wgLat, double wgLon, out double mgLat, out double mgLon)
        {
            if (outOfChina(wgLat, wgLon))
            {
                mgLat = wgLat;
                mgLon = wgLon;
                return;
            }
            double dLat = transformLat(wgLon - 105.0, wgLat - 35.0);
            double dLon = transformLon(wgLon - 105.0, wgLat - 35.0);
            double radLat = wgLat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            mgLat = wgLat + dLat;
            mgLon = wgLon + dLon;
        }

        static bool outOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        static double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        static double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }

        /// <summary>
        /// WGS-84 到 GCJ-02
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void WGS84toGCJ02(ref double x, ref double y)
        {
            double outx;
            double outy;
            transform(y, x,out outy,out outx);
            x = outx;
            y = outy;
        }

        /// <summary>
        /// GCJ-02 到 WGS-84
        /// 这个没有现成算法。目前根据WGS84toGCJ02算一个便宜了，然后反算回去。
        /// 在精度要求不高的情况下，但勉强能用
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void GCJ02toWGS84(ref double x, ref double y)
        {
            double outx;
            double outy;
            transform(y, x, out outy, out outx);
            x = x - (outx - x);
            y = y - (outy - y);
        }
    }

    /// <summary>
    /// 坐标转换工具
    /// </summary>
    class CoordSysTranslator
    {
        /// <summary>
        /// 投影转换对象。主要用于不同投影坐标系之间的转换。
        /// </summary>
        soPJTranslator pjTranslator = new soPJTranslator();

        /// <summary>
        /// wgs84坐标
        /// </summary>
        soPJCoordSys WGS84CoordSys = null;

        /// <summary>
        /// 本地坐标
        /// </summary>
        soPJCoordSys LocalCoordSys = null;

        /// <summary>
        /// 原点对应纬度值
        /// </summary>
        double Latitude_Of_Origin;

        public CoordSysTranslator()
        {
            WGS84CoordSys = new soPJCoordSys();
            WGS84CoordSys.Type = sePJCoordSysType.scPCS_LONGITUDE_LATITUDE;

            soPJGeoCoordSys geoCoordSys = new soPJGeoCoordSys();
            geoCoordSys.Type = sePJGeoCoordSysType.scGCS_WGS_1984;

            WGS84CoordSys.GeoCoordSys = geoCoordSys;
        }

        public void setLocalCoordSys(soPJCoordSys LocalCoordSys)
        {
            this.LocalCoordSys = LocalCoordSys;
            Latitude_Of_Origin = LocalCoordSys.PJParams.CentralParallel;

            pjTranslator.PJCoordSysSrc = WGS84CoordSys;
            pjTranslator.PJCoordSysDes = LocalCoordSys;

            pjTranslator.Create();
        }

        /// <summary>
        /// 坐标转换
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool convert(ref soPoint point)
        {
            double x = point.x;
            double y = point.y;
            //国内电子地图数据都是偏移过得，这里将偏移坐标（GCJ02）转换成WGS84
           // EvilTransform.GCJ02toWGS84(ref x, ref y);
            point.x = x;
            point.x = x;

            point.y -= Latitude_Of_Origin; //soPJTranslator目前不支持原点纬度参数，这里自己处理下
            return pjTranslator.Convert(point);
        }

        /// <summary>
        /// 坐标转换
        /// </summary>
        /// <param name="poiInfo"></param>
        /// <returns></returns>
        public bool convert(ref PoiInfo poiInfo)
        {
              soPoint point = new soPoint();
              point.x = poiInfo.x;
              point.y = poiInfo.y;
              bool sec = convert(ref point);
              poiInfo.x = point.x;
              poiInfo.y = point.y;
              return sec;
        }
    }
}
