using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    /**/
    /// <summary>
    /// 可以执行多步撤消操作的TextBox
    /// </summary>
    public partial class TextBoxEx : TextBox
    {
        //Undo/Redo members
        private List<UndoRedoInfo> mUndoList = new List<UndoRedoInfo>();
        private Stack<UndoRedoInfo> mRedoStack = new Stack<UndoRedoInfo>();
        private bool mIsUndo = false;
        private UndoRedoInfo mLastInfo = new UndoRedoInfo("", 0);
        private int mMaxUndoRedoSteps = 50;
        /**/
        /// <summary>
        /// The on text changed overrided.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (!mIsUndo)
            {
                mRedoStack.Clear();
                mUndoList.Insert(0, mLastInfo);
                this.LimitUndo();
                mLastInfo = new UndoRedoInfo(Text, SelectionStart);
            }
            //Invalidate();
        }
        /**/
        /// <summary>
        /// 设置和获取撤消操作允许的最大步数
        /// </summary>
        public int MaxUndoRedoSteps
        {
            set { mMaxUndoRedoSteps = value; }
            get { return mMaxUndoRedoSteps; }
        }
        /**/
        /// <summary>
        /// 清空Undo、Redo操作信息
        /// </summary>
        public void ClearUndoAndRedo()
        {
            mUndoList.Clear();
            mRedoStack.Clear();
        }
        /**/
        /// <summary>
        /// 判断是否可以进行Undo操作
        /// </summary>
        public new bool CanUndo
        {
            get { return mUndoList.Count > 0 && this.TextLength > 0 && this.TextLength < Glob.TextLen; }
        }
        /**/
        /// <summary>
        /// 判断是否可以进行Redo操作
        /// </summary>
        public bool CanRedo
        {
            get { return mRedoStack.Count > 0; }
        }
        /**/
        /// <summary>
        /// 撤消操作
        /// </summary>
        public new void Undo()
        {
            if (!CanUndo)
                return;
            mIsUndo = true;
            mRedoStack.Push(new UndoRedoInfo(Text, SelectionStart));
            UndoRedoInfo info = (UndoRedoInfo)mUndoList[0];
            mUndoList.RemoveAt(0);
            Text = info.Text;
            SelectionStart = info.CursorLocation;
            ScrollToCaret();
            mLastInfo = info;
            mIsUndo = false;
        }
        /**/
        /// <summary>
        /// 重复操作
        /// </summary>
        public void Redo()
        {
            if (!CanRedo)
                return;
            mIsUndo = true;
            mUndoList.Insert(0, new UndoRedoInfo(Text, SelectionStart));
            LimitUndo();
            UndoRedoInfo info = (UndoRedoInfo)mRedoStack.Pop();
            Text = info.Text;
            SelectionStart = info.CursorLocation;
            ScrollToCaret();
            mIsUndo = false;
        }
        private void LimitUndo()
        {
            while (mUndoList.Count > mMaxUndoRedoSteps)
            {
                mUndoList.RemoveAt(mMaxUndoRedoSteps);
            }
        }
        private class UndoRedoInfo
        {
            public UndoRedoInfo(string text, int cursorLoc)
            {
                Text = text;
                CursorLocation = cursorLoc;
            }
            public readonly int CursorLocation;
            public readonly string Text;
        }
    }
}
