using System.ComponentModel;
using System.Windows;

namespace CommunityToolkitStudy.Wpf.Views;

/// <summary>
/// 添付プロパティで DataContext を初期化します
/// </summary>
public static class ViewModelLocator
{
    public static bool GetAutoWireViewModel(DependencyObject obj) =>
        (bool)obj.GetValue(AutoWireViewModelProperty);

    public static void SetAutoWireViewModel(DependencyObject obj, bool value) =>
        obj.SetValue(AutoWireViewModelProperty, value);

    public static readonly DependencyProperty AutoWireViewModelProperty =
        DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.NotDataBindable, AutoWireViewModelChanged));

    private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(d))
            return;

        if (d is not FrameworkElement element)
            return;

        if (e.NewValue is bool value && value)
        {
            var viewType = element.GetType();
            element.DataContext = App.Current.GetViewModel(viewType);
        }
    }
}
