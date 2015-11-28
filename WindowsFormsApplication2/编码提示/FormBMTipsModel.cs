using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication2.编码提示
{
    public class FormBMTipsModel:IDisposable
    {
        #region 属性

        public State ReadState;

        public List<List<string>> Dic = new List<List<string>>();

        /// <summary>
        /// 词库文件目录
        /// </summary>
        public string DicPath { get { return Application.StartupPath + "\\bm.txt"; }}

        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public FormBMTipsModel()
        {
            ReadState = State.Normal;
            GetDic();
        }

        #region 方法

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <returns></returns>
        private bool FindFile()
        {
            if (File.Exists(DicPath))
            {
                return true;
            }
            else
            {
                ReadState = State.FileNotExist;
                return false;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <returns></returns>
        private string ReadFile()
        {
            if (FindFile())
            {
                using (var streamReader = new StreamReader(DicPath, Encoding.Default))
                {
                    var s = streamReader.ReadToEnd();
                    if (s.Length > 0)
                    {
                        return s;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 用正则获取词库文件
        /// </summary>
        /// <returns></returns>
        private List<List<string>> GetDic()
        {
            ReadState = State.Doing;
            var s = ReadFile();
            if (s == null) { ReadState = State.CanNotRead; return null; }
            //读取每行的数据
            var regex = new Regex(@"[a-z]{1,4}.+");
            var match = regex.Matches(s);
            //判断读取内容是否存在
            if (match.Count <= 0) { ReadState = State.CanNotFind; return null;}
            //获取数据
            var lists = match.Cast<object>()
                     .Select(item => item.ToString().Trim().Split(' '))
                     .Where(split => split.Length > 1)
                     .Select(split => split.ToList());
            //将数据填充
            foreach (var list in lists)
            {
                Dic.Add(list );
            }
            ReadState = State.Done;
            return Dic;
        }

        public void Dispose()
        {
            Dic.Clear();
        }
        #endregion
    }
}
