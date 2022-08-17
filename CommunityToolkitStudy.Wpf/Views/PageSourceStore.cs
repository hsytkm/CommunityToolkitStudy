using CommunityToolkitStudy.Wpf.Services;
using CommunityToolkitStudy.Wpf.Views.Mvvm.ComponentModel;
using CommunityToolkitStudy.Wpf.Views.Mvvm.DependencyInjection;
using CommunityToolkitStudy.Wpf.Views.Mvvm.Input;
using CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

namespace CommunityToolkitStudy.Wpf.Views;

internal static class PageSourceStore
{
    internal static IReadOnlyList<IPageSourceProvider> All { get; } = new IPageSourceProvider[]
    {
        //new PageSourceProvider<IMessenger2Page>(),

        // Mvvm.ComponentModel
        new PageSourceProvider<INotifyPropertyChanged1Page>(),
        new PageSourceProvider<ObservableProperty1Page>(),
        new PageSourceProvider<ObservableProperty2Page>(),
        new PageSourceProvider<ObservableValidator1Page>(),
        new PageSourceProvider<ObservableRecipient1Page>(),

        // Mvvm.DependencyInjection
        new PageSourceProvider<DependencyInjection1Page>(),

        // Mvvm.Input
        new PageSourceProvider<RelayCommand1Page>(),
        new PageSourceProvider<AsyncRelayCommand1Page>(),

        // Mvvm.Messaging
        new PageSourceProvider<IMessenger1Page>(),
        new PageSourceProvider<IMessenger2Page>(),


    };
}
