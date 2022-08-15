using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityToolkitStudy.Wpf.Services;

public interface IViewModelProvider
{
    object? GetViewModel<TView>() where TView : FrameworkElement;
}

internal sealed class MyServiceProviderSource
{
    readonly IServiceCollection _services = new ServiceCollection();
    readonly Dictionary<Type, Type> _viewToViewModelBindDict = new();

    internal void AddSingleton<T>() where T : class => _services.AddSingleton<T>();

    internal void AddTransient<T>() where T : class => _services.AddTransient<T>();

    internal void AddViewModel<TView, TViewModel>()
        where TView : FrameworkElement
        where TViewModel : class
    {
        AddTransient<TViewModel>();
        _viewToViewModelBindDict.Add(typeof(TView), typeof(TViewModel));
    }

    internal MyServiceProvider BuildServiceProvider()
    {
        var provider = _services.BuildServiceProvider();
        return new(provider, _viewToViewModelBindDict);
    }
}

/// <summary>
/// ServiceProvider that bind View and ViewModel.
/// </summary>
internal sealed class MyServiceProvider : IServiceProvider, IViewModelProvider
{
    readonly IServiceProvider _provider;
    readonly IReadOnlyDictionary<Type, Type> _viewToViewModelDict;

    public MyServiceProvider(IServiceProvider provider, IReadOnlyDictionary<Type, Type> viewToViewModelDict) =>
        (_provider, _viewToViewModelDict) = (provider, viewToViewModelDict);

    public object? GetService(Type serviceType) => _provider.GetService(serviceType);

    public object? GetViewModel<TView>()
        where TView : FrameworkElement
    {
        if (_viewToViewModelDict.TryGetValue(typeof(TView), out var vmType))
            return GetService(vmType);

        throw new KeyNotFoundException($"View Type is {typeof(TView).FullName}.");
    }
}
