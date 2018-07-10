using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication2.检查更新
{
    public class UpgradeModel
    {
        #region 事件

        public delegate void hasUp(object sender,bool b);

        public event hasUp HasUpdated;

        protected virtual void OnHasUpdated(bool b)
        {
            hasUp handler = HasUpdated;
            if (handler != null) handler(this,b);
            
        }
        
        #endregion
        #region 属性

        private Regex _getInfo = new Regex(@"\[taliove\].+\[\/taliove\]");
        private string _url = "http://www.taliove.com/gdq-download/updateinfo/";
        public string 获取 { set; get; }
        public string 标题 { set; get; }
        private Regex _getTitle = new Regex(@"(?<=\[title\]).+(?=\[\/title\])");

        public string 版本 { get; set; }
        private Regex _getVersion = new Regex(@"(?<=\[version\])\d+.\d+.\d+(?=.*?\[\/version\])");

        public string 说明 { set; get; }
        private Regex _getInstra = new Regex(@"(?<=\[info\]).+(?=\[\/info\])");

        public DateTime 日期 { set; get; }
        private Regex _getDate = new Regex(@"(?<=\[date\]).+(?=\[\/date\])");

        public string 更新内容 { get; set; }
        private Regex _getContext = new Regex(@"(?<=\[content\]).+(?=\[\/content\])");

        public string 其它信息 { set; get; }
        private Regex _getOther = new Regex(@"(?<=\[detail\]).+(?=\[\/detail\])");

        public List<int> Compare = new List<int>();
        public bool 是否有更新 = false;
        public bool 是否有异常 = false;

        #endregion

        public UpgradeModel()
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(!string.IsNullOrEmpty(标题) ? 标题 : "");
            sb.AppendLine(版本);
            sb.AppendLine("更新时间：" + 日期.ToShortDateString());
            sb.AppendLine(!string.IsNullOrEmpty(说明) ? "更新说明：\n" + 说明 :"");
            sb.AppendLine("更新内容：\n" + 更新内容);
            sb.AppendLine("其它说明：\n" + 其它信息);
            return sb.ToString();
        }

        /// <summary>
        ///     获取源数据
        /// </summary>
        public void GetWebRequest()
        {
            Compare.Clear();
            是否有异常 = false;
            Uri uri = new Uri(_url);
            WebRequest myReq = WebRequest.Create(uri);
            try
            {
                WebResponse result = myReq.GetResponse();
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.UTF8);
                string strHTML = readerOfStream.ReadToEnd();
                readerOfStream.Close();
                receviceStream.Close();
                result.Close();
                strHTML = strHTML.Replace("&lt;", "<").Replace("&gt;",">");
                获取 = _getInfo.Match(strHTML).ToString();
                版本 = _getVersion.Match(获取).ToString();
                //对版本进行判断
                var LocalVerion = Glob.VerInstance;
                if (CompareVer(LocalVerion, 版本))
                {
                    是否有更新 = true;
                    标题 = _getTitle.Match(获取).ToString();
                    说明 = _getInstra.Match(获取).ToString().Replace('|', '\n');
                    try
                    {
                        日期 = Convert.ToDateTime(_getDate.Match(获取).ToString());
                    }
                    catch
                    {
                        日期 = DateTime.Now;
                    }
                    更新内容 = _getContext.Match(获取).ToString().Replace('|', '\n');
                    其它信息 = _getOther.Match(获取).ToString().Replace('|','\n');
                }
                else
                {
                    是否有更新 = false;
                }
            }
            catch (Exception e)
            {
                是否有异常 = true;
                是否有更新 = false;
            }
            finally
            {
                OnHasUpdated(是否有更新);
            }
        }

        /// <summary>
        /// 比较版本是否有更新
        /// </summary>
        /// <param name="vL">本地版本</param>
        /// <param name="vN">网络版本</param>
        /// <returns></returns>
        private bool CompareVer(string vL, string vN)
        {
            var vLoc = SpliteVer(vL);
            var vNet = SpliteVer(vN);
            if (vNet[0] == -1) return false;
            for (int index = 0; index < vNet.Length; index++)
            {
                var iN = vNet[index];
                var iL = vLoc[index];
                Compare.Add(iN - iL);
            }
            return isUpgrade(Compare);
        }

        public bool isUpgrade(IEnumerable<int> ints)
        {
            var finds = ints.Count(o => o != 0);
            return finds != 0;
        }

        /// <summary>
        /// 获取各个小版本号
        /// </summary>
        /// <param name="ver"></param>
        /// <returns></returns>
        private int[] SpliteVer(string ver)
        {
            var sp = ver.Split('.');
            if (sp.Length != 3) return null;
            var sv = new int[3];
            int.TryParse(sp[0], out sv[0]);
            int.TryParse(sp[1], out sv[1]);
            int.TryParse(sp[2], out sv[2]);
            return sv;
        }

    }
}
