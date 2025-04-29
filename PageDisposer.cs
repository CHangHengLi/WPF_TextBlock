using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_TextBlock
{
    /// <summary>
    /// 页面资源释放器，用于彻底清理页面资源，防止内存泄漏
    /// </summary>
    public static class PageDisposer
    {
        /// <summary>
        /// 尝试释放页面及其所有子控件的资源
        /// </summary>
        /// <param name="page">需要释放的页面</param>
        public static void DisposePage(Page page)
        {
            if (page == null) return;

            try
            {
                // 解除页面的数据上下文
                page.DataContext = null;
                
                // 清理页面上的资源密集型对象
                CleanupResourceIntensiveObjects(page);
                
                // 简单处理常见控件
                ProcessCommonControls(page);
            }
            catch (Exception ex)
            {
                // 记录异常信息，但不影响正常流程
                System.Diagnostics.Debug.WriteLine($"释放页面资源时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 处理常见控件的资源释放
        /// </summary>
        private static void ProcessCommonControls(DependencyObject element)
        {
            if (element == null) return;

            try
            {
                // 针对特定控件类型处理
                if (element is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (element is ListBox listBox)
                {
                    listBox.SelectedIndex = -1;
                }
                else if (element is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1;
                }
                else if (element is ListView listView)
                {
                    listView.SelectedIndex = -1;
                }
                else if (element is DataGrid dataGrid)
                {
                    dataGrid.SelectedIndex = -1;
                    dataGrid.ItemsSource = null;
                }
                else if (element is ItemsControl itemsControl && !(element is ComboBox))
                {
                    itemsControl.ItemsSource = null;
                }
                
                // 递归处理子元素
                int childCount = VisualTreeHelper.GetChildrenCount(element);
                for (int i = 0; i < childCount; i++)
                {
                    ProcessCommonControls(VisualTreeHelper.GetChild(element, i));
                }
                
                // 处理逻辑树中的子元素
                if (element is Panel panel)
                {
                    foreach (UIElement child in panel.Children)
                    {
                        ProcessCommonControls(child);
                    }
                }
                else if (element is ContentControl contentControl && contentControl.Content is DependencyObject content)
                {
                    ProcessCommonControls(content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"处理控件时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 清理资源密集型对象
        /// </summary>
        private static void CleanupResourceIntensiveObjects(DependencyObject element)
        {
            try
            {
                // 处理图像资源
                if (element is Image image)
                {
                    image.Source = null;
                }
                
                // 处理媒体元素
                if (element is MediaElement mediaElement)
                {
                    mediaElement.Stop();
                    if (mediaElement.Source != null)
                    {
                        mediaElement.Close();
                        mediaElement.Source = null;
                    }
                }
                
                // 递归处理子元素
                int childCount = VisualTreeHelper.GetChildrenCount(element);
                for (int i = 0; i < childCount; i++)
                {
                    CleanupResourceIntensiveObjects(VisualTreeHelper.GetChild(element, i));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"清理资源密集型对象时出错: {ex.Message}");
            }
        }
    }
} 