using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WPF_TextBlock.Pages
{
    /// <summary>
    /// InlineElementsPage.xaml 的交互逻辑
    /// </summary>
    public partial class InlineElementsPage : Page
    {
        public InlineElementsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 处理超链接点击事件
        /// </summary>
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // 使用默认浏览器打开超链接
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }
    }
} 