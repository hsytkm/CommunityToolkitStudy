using System.Windows;
using System.Windows.Threading;
using CommunityToolkitStudy.Wpf.Services;
using CommunityToolkitStudy.Wpf.ViewModels;
using CommunityToolkitStudy.Wpf.Views;
using CommunityToolkitStudy.Wpf.Views.Mvvm.DependencyInjection;

namespace CommunityToolkitStudy.Wpf;

public sealed partial class App : Application
{
    public static T GetService<T>() where T : class
    {
        if (_services.GetService(typeof(T)) is not T service)
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        return service;
    }

    public static object GetViewModel<T>() where T : class
    {
        if (_services.GetViewModel<T>() is not { } viewModel)
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        return viewModel;
    }

    static readonly MyServiceProvider _services = ConfigureServices();

    static MyServiceProvider ConfigureServices()
    {
        var services = new MyServiceProviderSource();

        //services.AddSingleton<ModelMain>();
        services.AddTransient<DependencyInjection1ViewModel>();
        services.AddSingleton<DependencyInjection1Model>();

        // Views and ViewModels
        services.AddViewModel<PagesListBoxPage, PagesListBoxViewModel>();

        return services.BuildServiceProvider();
    }

    private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        var message = $"Caught an unhandled exception." + Environment.NewLine
            + $"{e.Exception.GetType()} : {e.Exception.Message}";

        MessageBox.Show(message, "Exception occurred", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
    }
}
