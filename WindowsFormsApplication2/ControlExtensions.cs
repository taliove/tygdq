using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    internal static class ControlExtensions
    {
        public static void UIThread(this Control control, MethodInvoker method)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(method);
                return;
            }
            method.Invoke();
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
    public sealed class ExtensionAttribute : Attribute
    {
    }
}