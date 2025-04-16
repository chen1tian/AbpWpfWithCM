using System.Windows;
using System.Windows.Interop;

namespace AbpWpfWithCM.WpfApp.Helpers
{
    public static class MsgHelper
    {
        /// <summary>
        /// 警告提示框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowWarning(string msg)
        {
            MessageBox.Show(msg, "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// 消息提示框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowInfo(string msg)
        {
            MessageBox.Show(msg, "信息", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 错误提示框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        /// <summary>
        /// 确认对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Confirm(string msg)
        {
            var rs = MessageBox.Show(msg, "信息", MessageBoxButton.YesNo, MessageBoxImage.Information);
            return rs.Equals(MessageBoxResult.Yes);
        }
    }
}
