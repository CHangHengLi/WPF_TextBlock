using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_TextBlock.Pages
{
    /// <summary>
    /// AdvancedUsagePage.xaml 的交互逻辑
    /// </summary>
    public partial class AdvancedUsagePage : Page
    {
        private int textBlockCount = 0;
        private Random random = new Random();

        public AdvancedUsagePage()
        {
            InitializeComponent();
            
            // 在组件初始化后，等待布局完成再设置文本
            this.Loaded += (s, e) => 
            {
                if (InputTextBox != null && DynamicTextBlock != null)
                {
                    DynamicTextBlock.Text = InputTextBox.Text;
                }
            };
        }

        /// <summary>
        /// 输入框文本变化事件处理
        /// </summary>
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // 添加空检查以避免空引用异常
            if (InputTextBox != null && DynamicTextBlock != null)
            {
                DynamicTextBlock.Text = InputTextBox.Text;
            }
        }

        /// <summary>
        /// 创建TextBlock按钮点击事件
        /// </summary>
        private void CreateTextBlockBtn_Click(object sender, RoutedEventArgs e)
        {
            // 清除初始提示文本
            if (DynamicTextBlockContainer.Children.Count == 1 &&
                DynamicTextBlockContainer.Children[0] is TextBlock textBlock &&
                textBlock.Text == "动态创建的TextBlock将显示在这里")
            {
                DynamicTextBlockContainer.Children.Clear();
            }

            textBlockCount++;

            // 创建新的TextBlock
            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Text = $"TextBlock #{textBlockCount} - 动态创建于 {DateTime.Now.ToString("HH:mm:ss")}";
            newTextBlock.Margin = new Thickness(0, 0, 0, 5);
            newTextBlock.TextWrapping = TextWrapping.Wrap;

            // 随机样式
            newTextBlock.FontSize = random.Next(12, 20);
            newTextBlock.FontWeight = random.Next(2) == 0 ? FontWeights.Normal : FontWeights.Bold;
            newTextBlock.FontStyle = random.Next(2) == 0 ? FontStyles.Normal : FontStyles.Italic;

            // 随机颜色
            byte r = (byte)random.Next(256);
            byte g = (byte)random.Next(256);
            byte b = (byte)random.Next(256);
            newTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(r, g, b));

            // 添加到容器
            DynamicTextBlockContainer.Children.Add(newTextBlock);
        }
    }
} 