# WPF TextBlock控件演示程序

这是一个基于.NET 8.0的WPF应用程序，用于演示WPF中TextBlock控件的各种功能和用法。该程序提供了一个交互式界面，展示了TextBlock控件的各种属性、格式化选项和高级功能。

![image](https://github.com/user-attachments/assets/7b91c295-717f-4a71-805d-e2e692f8aecb)

## 功能特点

该演示程序包含以下几个主要部分：

1. **基本属性**：展示TextBlock的基本属性，如字体、颜色、对齐方式等
2. **内联元素**：演示Run、Bold、Italic、Hyperlink等内联元素的用法
3. **文本换行与裁剪**：展示TextWrapping和TextTrimming属性的效果
4. **文本格式化**：演示文本格式化的各种方法和效果
5. **高级用法**：包括特殊布局、代码自定义、性能优化建议等

## 系统要求

- .NET 8.0 SDK或更高版本
- 支持WPF的Windows操作系统

## 如何运行

1. 确保已安装.NET 8.0 SDK
2. 使用Visual Studio 2022或更高版本打开解决方案文件`WPF_TextBlock.sln`
3. 编译并运行项目

也可以通过命令行运行：
```
dotnet build
dotnet run
```

## 项目结构

- **MainWindow.xaml**：主窗口，包含导航栏和内容区域
- **Pages/BasicPropertiesPage.xaml**：TextBlock基本属性演示
- **Pages/InlineElementsPage.xaml**：内联元素演示
- **Pages/TextWrappingPage.xaml**：文本换行与裁剪演示
- **Pages/FormattingPage.xaml**：文本格式化演示
- **Pages/AdvancedUsagePage.xaml**：高级用法演示

## 学习资源

本演示程序是根据WPF中TextBlock控件的详细文档创建的，更多信息请参考：

- [微软官方文档 - TextBlock类](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.textblock)
- [WPF TextBlock控件概述](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/controls/textblock-overview)

## 许可

该项目基于MIT许可证开源。 
