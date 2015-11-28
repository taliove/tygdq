using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;

namespace WindowsFormsApplication2
{
    public class WordInfo
    {
        public int start;
        public int end;
        public Color color;
        public int len;
    };

    public class DictInfo
    {
        public HashSet<string> ci; //所有以某字符开始的词表
        public HashSet<int> lens;         //所有以某字符开始的词长度列表
        public int Cong;
        public int Len;
    }

    /// <summary>
    /// 高速分词的工具，按最长的词来进行匹配
    /// create by hwjmyz ,2012-10-18
    /// </summary>
    public class WordInfoUtil
    {
        //为了提高效率，字典按如下结构建立：首字符， 然后是一个键值列表
        private SortedList<char, DictInfo> _dict = new SortedList<char, DictInfo>();

        /// <summary>
        /// 设置词库字符串（用回车分隔，每行一个词）
        /// </summary>
        /// <param name="ciku"></param>
        public void SetCiKu(string[] astr )
        {
            _dict.Clear();
           
            foreach (string s in astr)
            {
                try
                {
                    char firstChar = s[0];
                    DictInfo di;
                    if (_dict.ContainsKey(firstChar))
                    {
                        di = _dict[firstChar];
                    }
                    else
                    {
                        di = new DictInfo();
                        di.ci = new HashSet<string>();
                        di.lens = new HashSet<int>();
                        _dict.Add(firstChar, di);
                    }

                    try
                    {
                        di.ci.Add(s);
                    }
                    catch
                    {
                    }

                    try
                    {
                        di.lens.Add(s.Length);
                    }
                    catch
                    {
                    }
                }
                catch
                {
                    //出错的原因肯定是有重复，不管
                }
            }
        }

        /// <summary>
        /// 设置词库字符串 智能测词
        /// </summary>
        /// <param name="ciku"></param>
        public void SetCiKu()
        {
            _dict.Clear();

            var lists = Glob.BmAlls.Where(j => j.查询的字.Length > 1).ToList();
            var alls = lists.Select(o => o.查询的字).ToArray();
            foreach (var s in alls)
            {
                try
                {
                    
                    char firstChar = s[0];
                    DictInfo di;
                    if (_dict.ContainsKey(firstChar))
                    {
                        di = _dict[firstChar];
                    }
                    else
                    {
                        di = new DictInfo();
                        di.ci = new HashSet<string>();
                        di.lens = new HashSet<int>();
                        di.Cong = 1;
                        di.Len = lists.Find(o=>o.查询的字 == s).编码.Length;
                        _dict.Add(firstChar, di );
                    }

                    try
                    {
                        di.ci.Add(s);
                    }
                    catch
                    {
                    }

                    try
                    {
                        di.lens.Add(s.Length);
                    }
                    catch
                    {
                    }

                    try
                    {
                        var c = Glob.BmAlls.Find(o => o.查询的字 == s.ToString()).重数;
                        di.Cong = c;
                        di.Len = lists.Find(o => o.查询的字 == s).编码.Length;
                    }
                    catch (Exception)
                    {
                        di.Len = 1;
                        di.Cong = 1;
                    }
                }
                catch
                {
                    //出错的原因肯定是有重复，不管
                }
            }
        }

        /// <summary>
        /// 分词。 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<WordInfo> GetWordInfos(string text, Color color1, Color color2)
        {
            List<WordInfo> wordInfos = new List<WordInfo>();

            int i, n;
            n = text.Length;
            i = 0;

            while (i < n)
            {
                int j = 1;
                char c = text[i];
                if (_dict.ContainsKey(c))
                {
                    //在词库中找到的所有以该字符开始的词
                    DictInfo di = _dict[c];
                    int[] lens = di.lens.OrderByDescending(o => o).ToArray();
                    foreach (int k in lens)
                    {
                        if (i + k <= n)
                        {
                            string s = text.Substring(i, k);
                            if (di.ci.Contains(s))
                            {
                                //找到了最长的一个匹配的词
                                j = k;
                                Color cr = k > 2 ? color2 : color1;
                                wordInfos.Add(new WordInfo() { start = i, end = i + j - 1, color = cr });
                                break;
                            }
                        }
                    }

                    i += j;
                }
                else
                {
                    i++;
                }
            }

            return wordInfos;
        }

        /// <summary>
        /// 智能测词分词。 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<WordInfo> GetWordInfos(string text)
        {
            var wordInfos = new List<WordInfo>();

            var n = text.Length;
            var i = 0;

            while (i < n)
            {
                int j = 1;
                char c = text[i];
                if (_dict.ContainsKey(c))
                {
                    //在词库中找到的所有以该字符开始的词
                    DictInfo di = _dict[c];
                    int[] lens = di.lens.OrderByDescending(o => o).ToArray();
                    foreach (int k in lens)
                    {
                        if (i + k <= n)
                        {
                            string s = text.Substring(i, k);
                            if (di.ci.Contains(s))
                            {
                                //找到了最长的一个匹配的词
                                j = k;
                                wordInfos.Add(new WordInfo()
                                    {
                                        start = i,
                                        end = i + j - 1,
                                        color = Glob.BmColors[di.Cong > 4 ? 3 : di.Cong <= 0 ? 0 : di.Cong - 1],
                                        len = di.Len
                                    });
                                break;
                            }
                        }
                    }

                    i += j;
                }
                else
                {
                    i++;
                }
            }

            return wordInfos;
        }
    }

}
