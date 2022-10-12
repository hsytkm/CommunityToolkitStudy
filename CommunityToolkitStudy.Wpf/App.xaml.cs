using System.Windows;
using System.Windows.Threading;
using CommunityToolkitStudy.Wpf.Services;
using CommunityToolkitStudy.Wpf.ViewModels;
using CommunityToolkitStudy.Wpf.Views;
using CommunityToolkitStudy.Wpf.Views.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityToolkitStudy.Wpf;

public sealed partial class App : Application
{
    public static new App Current => (App)Application.Current;
    readonly MyServiceProvider _serviceProvider;

    public App()
    {
        _serviceProvider = ConfigureServices();
        Ioc.Default.ConfigureServices(_serviceProvider);

        // 実行完了が保障されません。ダメな実装です。
        Exit += (_, _) => _serviceProvider.Dispose();
    }

    static MyServiceProvider ConfigureServices()
    {
        var provider = new MyServiceProviderSource();
        var services = provider.Services;

        services.AddTransient<DependencyInjection1ViewModel>();
        services.AddSingleton<DependencyInjection1Model>();

        // Views and ViewModels
        provider.AddViewModel<PagesListBoxPage, PagesListBoxViewModel>();

        return provider.BuildServiceProvider();
    }

    public object GetViewModel(Type viewType)
    {
        if (_serviceProvider.GetViewModel(viewType) is not { } viewModel)
            throw new ArgumentException($"{viewType} needs to be registered in ConfigureServices within App.xaml.cs.");
        return viewModel;
    }

    public object GetViewModel<T>() where T : FrameworkElement =>
        GetViewModel(typeof(T));

    private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        var message = $"Caught an unhandled exception." + Environment.NewLine
            + $"{e.Exception.GetType()} : {e.Exception.Message}";

        MessageBox.Show(message, "Exception occurred", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
    }
}
