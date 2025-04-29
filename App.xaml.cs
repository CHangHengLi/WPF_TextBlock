using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WPF_TextBlock
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // 添加全局异常处理
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // 处理UI线程未处理的异常
            MessageBox.Show($"发生未处理的异常: {e.Exception.Message}\n\n堆栈跟踪:\n{e.Exception.StackTrace}",
                "应用程序错误", MessageBoxButton.OK, MessageBoxImage.Error);

            // 标记异常为已处理，防止应用程序崩溃
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // 处理非UI线程未处理的异常
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show($"发生严重错误: {(ex != null ? ex.Message : "未知错误")}\n\n应用程序即将关闭。",
                "严重错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}