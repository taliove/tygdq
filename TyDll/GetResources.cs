using System;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace TyDll
{
    public class GetResources
    {
        #region 常量
        /// <summary>
        /// 程序集的名称
        /// </summary>
        private static string CurrentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        #endregion

        #region 变量
        /// <summary>
        /// 当前程序集
        /// </summary>
        private static Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        #endregion

        #region 方法
        /// <summary>
        /// 在嵌入的资源文件中查找相应的图片
        /// </summary>
        /// <param name="name">资源图片的文件名称+扩展名</param>
        /// <returns></returns>
        public static Image GetImage(string name)
        {
            Image image = null;
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    StringBuilder sb = new StringBuilder();
                    if (name[0] != '.')
                        sb.Append(GetResources.CurrentAssemblyName + "." + name);
                    else
                        sb.Append(GetResources.CurrentAssemblyName + name);
                    using (Stream stream = CurrentAssembly.GetManifestResourceStream(sb.ToString()))
                    {
                        if (stream == null)
                            throw new Exception("加载资源文件失败，失败原因：可能丢失" + CurrentAssemblyName + ".dll文件。");
                        else
                            image = Image.FromStream(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AssemblyHelper.GetImage(string)->" + ex.Message);
                throw ex;
            }
            return image;
        }

        /// <summary>
        /// 从资源中获取文本
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetText(string name) {
            string text = null;
            try {
                if (!string.IsNullOrEmpty(name)) {
                    StringBuilder sb = new StringBuilder();
                    if (name[0] != '.')
                        sb.Append(GetResources.CurrentAssemblyName + "." + name);
                    else
                        sb.Append(GetResources.CurrentAssemblyName + name);
                    using (Stream stream = CurrentAssembly.GetManifestResourceStream(sb.ToString()))
                    {
                        if (stream == null)
                            throw new Exception("加载资源文件失败，失败原因：可能丢失" + CurrentAssemblyName + ".dll文件。");
                        else
                        {
                         //   text = StreamReader(stream);//Image.FromStream(stream);
                            StreamReader sr = new StreamReader(stream);
                            text = sr.ReadToEnd();
                            sr.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AssemblyHelper.GetImage(string)->" + ex.Message);
                throw ex;
            }
            return text;
        }
        #endregion
    }
}
