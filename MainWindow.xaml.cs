using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WPF_TextBlock
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // 跟踪当前显示的页面类型，避免重复导航
        private Type _currentPageType = null;
        // 标记窗口是否已完全加载
        private bool _isLoaded = false;

        public MainWindow()
        {
            InitializeComponent();

            // 添加窗口加载完成事件，确保在视觉树完全加载后再导航
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
            
            // 窗口加载完成事件
            // 确保第一个导航项被选中
            if (NavListBox.Items.Count > 0)
            {
                // 手动触发一次导航逻辑
                ListBoxItem firstItem = NavListBox.Items[0] as ListBoxItem;
                if (firstItem != null && firstItem.Tag != null)
                {
                    NavigateToPage(firstItem.Tag.ToString());
                }
            }
        }

        private void ContentFrame_Loaded(object sender, RoutedEventArgs e)
        {
            // Frame加载完成事件，此时Frame已经完全初始化
            try
            {
                // 确保ContentFrame不为null
                if (sender is Frame frame)
                {
                    // 设置Frame的JournalOwnership，防止导航问题
                    frame.JournalOwnership = JournalOwnership.OwnsJournal;

                    // 添加导航事件处理
                    frame.Navigated -= Frame_Navigated;
                    frame.Navigated += Frame_Navigated;
                    frame.NavigationFailed -= Frame_NavigationFailed;
                    frame.NavigationFailed += Frame_NavigationFailed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ContentFrame加载错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            // 导航成功后的处理
            if (e.Content is Page page)
            {
                _currentPageType = page.GetType();
            }
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            MessageBox.Show($"导航失败: {e.Exception.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        /// <summary>
        /// 根据页面Tag导航到指定页面
        /// </summary>
        private bool NavigateToPage(string pageTag)
        {
            if (string.IsNullOrEmpty(pageTag)) return false;

            try
            {
                // 确定页面类型
                Type pageType = null;
                switch (pageTag)
                {
                    case "BasicPropertiesPage":
                        pageType = typeof(Pages.BasicPropertiesPage);
                        break;
                    case "InlineElementsPage":
                        pageType = typeof(Pages.InlineElementsPage);
                        break;
                    case "TextWrappingPage":
                        pageType = typeof(Pages.TextWrappingPage);
                        break;
                    case "FormattingPage":
                        pageType = typeof(Pages.FormattingPage);
                        break;
                    case "AdvancedUsagePage":
                        pageType = typeof(Pages.AdvancedUsagePage);
                        break;
                    default:
                        return false;
                }

                // 如果当前已经显示的是选中的页面类型，不重复导航
                if (_currentPageType == pageType) return true;

                // 创建页面实例
                Page page = (Page)Activator.CreateInstance(pageType);
                if (page == null) return false;

                // 清除导航历史
                while (ContentFrame.NavigationService?.CanGoBack == true)
                {
                    ContentFrame.NavigationService.RemoveBackEntry();
                }

                // 设置新页面
                ContentFrame.Content = page;
                
                // 更新当前页面类型
                _currentPageType = pageType;
                
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"导航错误: {ex.Message}");
                return false;
            }
        }

        private void NavListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isLoaded) return; // 确保窗口已完全加载
            
            ListBoxItem selectedItem = NavListBox.SelectedItem as ListBoxItem;
            if (selectedItem == null || selectedItem.Tag == null) return;

            NavigateToPage(selectedItem.Tag.ToString());
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