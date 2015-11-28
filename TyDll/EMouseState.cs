using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TyDll
{
    /// <summary>
    /// 鼠标状态
    /// </summary>
    public enum EMouseState
    {
        /// <summary>
        /// 默认
        /// </summary>
        Normal,
        /// <summary>
        /// 鼠标划过
        /// </summary>
        Move,
        /// <summary>
        /// 鼠标按下
        /// </summary>
        Down,
        /// <summary>
        /// 鼠标释放
        /// </summary>
        Up,
        /// <summary>
        /// 鼠标离开
        /// </summary>
        Leave,
    }
}
