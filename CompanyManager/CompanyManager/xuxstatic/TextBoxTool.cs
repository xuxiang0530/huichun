using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CompanyManager
{
    public static class TextBoxTool
    {
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]

        private static extern Int32 SendMessage
          (IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        /// <summary>
        /// 为TextBox设置提示文字
        /// </summary>
        /// <param name="textBox">TextBox</param>
        /// <param name="watermark">提示文字内容</param>
        public static void SetWatermark(this TextBox textBox, string watermark)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermark);
        }
        /// <summary>
        /// 清除提示文字
        /// </summary>
        /// <param name="textBox">TextBox</param>
        public static void ClearWatermark(this TextBox textBox)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, string.Empty);
        }
    }
}
