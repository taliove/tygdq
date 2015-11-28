using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace WindowsFormsApplication2
{
    public class Theme
    {
        /// <summary>
        /// 主题控制
        /// </summary>
        public static bool ThemeApply = false;
        /// <summary>
        /// 主题背景颜色
        /// </summary>
        public static Color ThemeColorBG = Color.FromArgb(74, 129, 97);
        /// <summary>
        /// 主题前景色
        /// </summary>
        public static Color ThemeColorFC = Color.White;

        /// <summary>
        /// 纯色
        /// </summary>
        public static Color ThemeBG = Color.DimGray;
        /// <summary>
        /// 是否应用了主题背景
        /// </summary>
        public static bool isBackBmp = false;
        /// <summary>
        /// 主题背景
        /// </summary>
        public static string ThemeBackBmp = "";

        /// <summary>
        /// 是否启用了预览
        /// </summary>
        public static bool ReView = false;
    }

}
