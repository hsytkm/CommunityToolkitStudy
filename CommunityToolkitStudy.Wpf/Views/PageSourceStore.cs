using System.Collections.Generic;
using CommunityToolkitStudy.Wpf.Services;
using CommunityToolkitStudy.Wpf.Views.Mvvm.Input;
using CommunityToolkitStudy.Wpf.Views.Mvvm.ComponentModel;

namespace CommunityToolkitStudy.Wpf.Views;

internal static class PageSourceStore
{
    internal static IReadOnlyList<IPageSourceProvider> All { get; } = new IPageSourceProvider[]
    {
        // Mvvm.ComponentModel
        new PageSourceProvider<INotifyPropertyChanged1Page>(),
        new PageSourceProvider<ObservableProperty1Page>(),
        new PageSourceProvider<ObservableProperty2Page>(),
        new PageSourceProvider<ObservableValidator1Page>(),
        new PageSourceProvider<ObservableRecipient1Page>(),

        // Mvvm.Input
        new PageSourceProvider<RelayCommand1Page>(),
        new PageSourceProvider<AsyncRelayCommand1Page>(),
        
    };
}
