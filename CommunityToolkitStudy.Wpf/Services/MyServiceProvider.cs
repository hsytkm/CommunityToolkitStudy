using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityToolkitStudy.Wpf.Services;

public interface IViewModelProvider
{
    object? GetViewModel(Type viewType);
    object? GetViewModel<TView>() where TView : FrameworkElement;
}

internal sealed class MyServiceProviderSource
{
    public ServiceCollection Services { get; } = new();
    readonly Dictionary<Type, Type> _viewToViewModelBindDict = new();

    internal void AddViewModel<TView, TViewModel>()
        where TView : FrameworkElement
        where TViewModel : class
    {
        Services.AddTransient<TViewModel>();
        _viewToViewModelBindDict.Add(typeof(TView), typeof(TViewModel));
    }

    internal MyServiceProvider BuildServiceProvider()
    {
        var provider = Services.BuildServiceProvider();
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

    public object? GetViewModel(Type viewType)
    {
        if (_viewToViewModelDict.ContainsKey(viewType))
        {
            var vmType = _viewToViewModelDict[viewType];
            return GetService(vmType);
        }

        throw new KeyNotFoundException($"View Type is {viewType.FullName}.");
    }

    public object? GetViewModel<TView>() where TView : FrameworkElement =>
        GetViewModel(typeof(TView));

}
