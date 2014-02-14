using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LandMap
{

    /// <summary>
    /// 类型节点
    /// </summary>
    class TypeCodeNode
    {
        /// <summary>
        /// 类似代码
        /// </summary>
        public string code = "";

        /// <summary>
        /// 代码名称
        /// </summary>
        public string name = "";

        /// <summary>
        /// 子节点
        /// </summary>
        public List<TypeCodeNode> subNodes = new List<TypeCodeNode>();

        public void clearSubNode()
        {
            subNodes.Clear();
        }

        public TypeCodeNode addSubNode(String code, String name)
        {
            TypeCodeNode subNode = new TypeCodeNode();
            subNode.code = code;
            subNode.name = name;
            this.subNodes.Add(subNode);

            return subNode;
        }


        public override string ToString()
        {
            //if (code.Length == 3)
            //{
            //    return String.Format("        {0}({1})", name, code);
            //}
            //else if (code.Length == 2)
            //{
            //    return String.Format("    {0}({1})", name, code);
            //}
            return String.Format("{0}({1})", name, code);
        }
    }

    /// <summary>
    /// 类型表
    /// </summary>
    class TypeCodeTable
    {
        /// <summary>
        /// 根节点
        /// </summary>
        TypeCodeNode rootTypeCodeNode = new TypeCodeNode();


        public TypeCodeTable()
        {
            loadDefaultNew();
        }

        /// <summary>
        /// 加载默认类型，参照《转化后地类》
        /// </summary>
        /// <returns></returns>
        public Boolean loadDefaultNew()
        {
            TypeCodeNode onenode = rootTypeCodeNode.addSubNode("1", "住宅");

            TypeCodeNode twonode = onenode.addSubNode("11", "住宅用地");

            twonode.addSubNode("111", "城镇住宅用地");
            twonode.addSubNode("112", "农村宅基地");


            //////////////////////////////////////////////////////////////////////////

            onenode = rootTypeCodeNode.addSubNode("2", "绿地");

            twonode = onenode.addSubNode("21", "农田");

            twonode.addSubNode("211", "水田");
            twonode.addSubNode("212", "水浇地");
            twonode.addSubNode("213", "旱地");

            twonode = onenode.addSubNode("22", "林地及果园");

            twonode.addSubNode("221", "果园");
            twonode.addSubNode("222", "其他园地");
            twonode.addSubNode("223", "有林地");
            twonode.addSubNode("224", "其他林地");

            twonode = onenode.addSubNode("23", "草地");

            twonode.addSubNode("231", "人工草地");
            twonode.addSubNode("232", "其他草地");

            twonode = onenode.addSubNode("24", "旅游与休闲用地");

            twonode.addSubNode("241", "公园绿地");
            twonode.addSubNode("242", "风景名胜");
            twonode.addSubNode("243", "寺庙或教堂");

            //////////////////////////////////////////////////////////////////////////
            onenode = rootTypeCodeNode.addSubNode("3", "水域用地");
            twonode = onenode.addSubNode("31", "水面");

            twonode.addSubNode("311", "河流水面");
            twonode.addSubNode("312", "湖泊水面");
            twonode.addSubNode("313", "水库水面");
            twonode.addSubNode("314", "坑塘水面");
            twonode.addSubNode("315", "沟渠");

            twonode = onenode.addSubNode("32", "其他水域用地");

            twonode.addSubNode("321", "沿海滩涂");
            twonode.addSubNode("322", "内陆滩涂");
            twonode.addSubNode("323", "水工建筑用地");


            //////////////////////////////////////////////////////////////////////////

            onenode = rootTypeCodeNode.addSubNode("4", "工矿与商服用地");

            twonode = onenode.addSubNode("41", "工业与采矿用地");
            twonode.addSubNode("411", "工业用地");
            twonode.addSubNode("412", "仓储用地");
            twonode.addSubNode("413", "采矿用地");

            twonode = onenode.addSubNode("42", "商业与服务用地");

            twonode.addSubNode("421", "批发零售用地");
            twonode.addSubNode("422", "商务金融用地");
            twonode.addSubNode("423", "住宿餐饮及其他服务用地");

            //////////////////////////////////////////////////////////////////////////

            onenode = rootTypeCodeNode.addSubNode("5", "公共服务与公共管理用地");

            twonode = onenode.addSubNode("51", "商业与服务用地");

            twonode.addSubNode("511", "科教用地");
            twonode.addSubNode("512", "医卫慈善用地");
            twonode.addSubNode("513", "文体娱乐用地");
            twonode.addSubNode("514", "机关及其他用地");

            twonode = onenode.addSubNode("52", "交通运输与其他基础设施用地");

            twonode.addSubNode("521", "铁路用地");
            twonode.addSubNode("522", "公路用地");
            twonode.addSubNode("523", "其他道路用地");
            twonode.addSubNode("524", "机场用地");
            twonode.addSubNode("525", "港口码头用地");

            //////////////////////////////////////////////////////////////////////////

            onenode = rootTypeCodeNode.addSubNode("6", "未利用地");
            twonode = onenode.addSubNode("61", "暂未利用土地");

            twonode.addSubNode("611", "空闲地及设施农用地");
            twonode.addSubNode("612", "盐碱地");
            twonode.addSubNode("613", "沼泽地");

            twonode.addSubNode("614", "沙地");
            twonode.addSubNode("615", "裸地");


            return true;
        }

        /// <summary>
        /// 加载默认类型，参照《土地利用现状分类标准》
        /// </summary>
        /// <returns></returns>
        public Boolean loadDefault()
        {
            //oneTypeCodes = new List<TypeCodeNode>();
            //TypeCodeNode onenode = new TypeCodeNode();
            //onenode.code = "01";
            //onenode.name = "耕地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //TypeCodeNode twonode = new TypeCodeNode();
            //twonode.code = "011";
            //twonode.name = "水田";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "012";
            //twonode.name = "水浇地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "013";
            //twonode.name = "旱地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "02";
            //onenode.name = "园地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "021";
            //twonode.name = "果园";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "022";
            //twonode.name = "茶园";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "023";
            //twonode.name = "其他园地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "03";
            //onenode.name = "林地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "031";
            //twonode.name = "有林地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "032";
            //twonode.name = "灌木林地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "033";
            //twonode.name = "其它林地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "04";
            //onenode.name = "草地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "041";
            //twonode.name = "天然牧草地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "042";
            //twonode.name = "人工牧草地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "043";
            //twonode.name = "其它草地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "05";
            //onenode.name = "商服用地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "051";
            //twonode.name = "批发零售用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "052";
            //twonode.name = "住宿餐饮用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "053";
            //twonode.name = "商务金融用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "054";
            //twonode.name = "其它商服用地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "06";
            //onenode.name = "工矿仓储用地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "061";
            //twonode.name = "工业用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "062";
            //twonode.name = "采矿用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "063";
            //twonode.name = "仓储用地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "07";
            //onenode.name = "住宅用地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "071";
            //twonode.name = "城镇住宅用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "072";
            //twonode.name = "农村宅基地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "08";
            //onenode.name = "公共管理与公共服务用地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "081";
            //twonode.name = "机关团体用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "082";
            //twonode.name = "新闻出版用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "083";
            //twonode.name = "科教用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "084";
            //twonode.name = "医卫慈善用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "085";
            //twonode.name = "文体娱乐用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "086";
            //twonode.name = "公共设施用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "087";
            //twonode.name = "公园与绿地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "088";
            //twonode.name = "风景名胜设施用地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "09";
            //onenode.name = "特殊用地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "091";
            //twonode.name = "军事设施用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "092";
            //twonode.name = "使领馆用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "093";
            //twonode.name = "监教场所用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "094";
            //twonode.name = "宗教用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "095";
            //twonode.name = "殡葬用地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "10";
            //onenode.name = "交通运输用地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "101";
            //twonode.name = "铁路用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "102";
            //twonode.name = "公路用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "103";
            //twonode.name = "街巷用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "104";
            //twonode.name = "农村道路";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "105";
            //twonode.name = "机场用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "106";
            //twonode.name = "港口码头用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "107";
            //twonode.name = "管道运输用地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "11";
            //onenode.name = "水域及水利设施用地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "111";
            //twonode.name = "河流水面";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "112";
            //twonode.name = "湖泊水面";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "113";
            //twonode.name = "水库水面";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "114";
            //twonode.name = "坑塘水面";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "115";
            //twonode.name = "沿海滩涂";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "116";
            //twonode.name = "内陆滩涂";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "117";
            //twonode.name = "沟渠";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "118";
            //twonode.name = "水工建筑用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "119";
            //twonode.name = "冰川及永久积雪";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);

            ////////////////////////////////////////////////////////////////////////////

            //onenode = new TypeCodeNode();
            //onenode.code = "12";
            //onenode.name = "其它土地";
            //onenode.subNodes = new List<TypeCodeNode>();

            //twonode = new TypeCodeNode();
            //twonode.code = "121";
            //twonode.name = "空闲地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "122";
            //twonode.name = "设施农用地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "123";
            //twonode.name = "田坎";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "124";
            //twonode.name = "盐碱地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "125";
            //twonode.name = "沼泽地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "126";
            //twonode.name = "沙地";
            //onenode.subNodes.Add(twonode);

            //twonode = new TypeCodeNode();
            //twonode.code = "127";
            //twonode.name = "裸地";
            //onenode.subNodes.Add(twonode);

            //oneTypeCodes.Add(onenode);


            return true;
        }

        /// <summary>
        /// 从文件中读取类型表
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Boolean loadFromFile(String filename)
        {
            return false;
        }

        public List<TypeCodeNode> getAllSubNode(TypeCodeNode node)
        {
            List<TypeCodeNode> all = new List<TypeCodeNode>();
            foreach (TypeCodeNode subnode in node.subNodes)
            {
                all.Add(subnode);
                List<TypeCodeNode> allSubNode = getAllSubNode(subnode);
                all.AddRange(allSubNode);
            }
            return all;
        }

        public List<TypeCodeNode> getNodeByLevel(TypeCodeNode node, int level)
        {
            List<TypeCodeNode> all = new List<TypeCodeNode>();
            foreach (TypeCodeNode subnode in node.subNodes)
            {
                if (subnode.code.Length == level)
                {
                    all.Add(subnode);
                }

                if (subnode.code.Length < level)
                {
                    List<TypeCodeNode> allSubNode = getNodeByLevel(subnode,level);
                    all.AddRange(allSubNode);
                }

            }
            return all;
        }

        /// <summary>
        /// 一级分类
        /// </summary>
        public TypeCodeNode[] OneTypeCodes
        {
            get
            {
                return this.rootTypeCodeNode.subNodes.ToArray();
            }
        }

        public TypeCodeNode[] getNodeByLevel(int level)
        {
            List<TypeCodeNode> nodes = getNodeByLevel(rootTypeCodeNode, level);
            return nodes.ToArray();

        }

        /// <summary>
        /// 所有分类，包括一级分类和二级分类
        /// </summary>
        public TypeCodeNode[] AllTypeCodes
        {
            get
            {
                List<TypeCodeNode> all = getAllSubNode(rootTypeCodeNode);
                return all.ToArray();
            }
        }

        public TypeCodeNode getSubNodeByCode(TypeCodeNode node, string code)
        {
            foreach (TypeCodeNode subnode in node.subNodes)
            {
                if (String.Compare(subnode.code, code) == 0)
                {
                    return subnode;
                }
                TypeCodeNode subNode = getSubNodeByCode(subnode, code);
                if (subNode != null)
                {
                    return subNode;
                }
            }
            return null;
        }

        public TypeCodeNode getSubNodeByName(TypeCodeNode node, string name)
        {
            foreach (TypeCodeNode subnode in node.subNodes)
            {
                if (String.Compare(subnode.name, name) == 0)
                {
                    return subnode;
                }
                TypeCodeNode subNode = getSubNodeByName(subnode, name);
                if (subNode != null)
                {
                    return subNode;
                }
            }
            return null;
        }

        public TypeCodeNode getNodeByCode(string code)
        {
            if (rootTypeCodeNode == null)
            {
                return null;
            }

            return getSubNodeByCode(rootTypeCodeNode, code);
        }

        public TypeCodeNode getNodeByName(String name)
        {
            if (rootTypeCodeNode == null)
            {
                return null;
            }

            return getSubNodeByName(rootTypeCodeNode, name);
        }

        public string getNameByCode(string code)
        {
            TypeCodeNode node = getNodeByCode(code);
            if (node == null)
            {
                return "";
            }
            return node.name;
        }

        public string getCodeByName(String name)
        {
            TypeCodeNode node = getNodeByName(name);
            if (node == null)
            {
                return "";
            }
            return node.code;
        }
    }
}
