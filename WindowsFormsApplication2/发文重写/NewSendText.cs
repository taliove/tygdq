using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    public class NewSendText
    {
        public static bool 发文状态;
        public static string 标题;
        public static string 文章全文 = "";
        public static string 发文全文 = "";
        public static string 类型;
        public static bool 是否乱序;
        //乱序全段不重复
        public static bool 乱序全段不重复 = false;

        public static bool 是否一句结束;
        public static int 起始段号 = 1;
        public static int 字数;//发送字数
        public static int 标记;
        public static int 已发段数 = 0;

        public static bool 是否周期;
        public static int 周期;
        public static int 周期计数 = 0;

        public static bool 是否独练;
        public static int 文章来源; // 0 自带文章 1自定义文章 2剪切板 3配置
        public static string 文章地址;
        public static int 已发字数 = 0;
        public static bool 是否自动 = false;
        public static string 当前配置序列;

        public static string 词组发送分隔符 = "，"; //用于词组的发送分隔，默认的为 逗号 ，此项目暂时不保存
        public static string[] 词组;

    }
}
