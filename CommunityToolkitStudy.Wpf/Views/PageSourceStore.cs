using System.Collections.Generic;
using CommunityToolkitStudy.Wpf.Services;
using CommunityToolkitStudy.Wpf.Views.Observables;

namespace CommunityToolkitStudy.Wpf.Views;

internal static class PageSourceStore
{
    internal static IReadOnlyList<IPageSourceProvider> All { get; } = new IPageSourceProvider[]
    {
        new PageSourceProvider<ObservableProperty1Page>(),
        new PageSourceProvider<ObservableProperty2Page>(),
        new PageSourceProvider<ObservableValidator1Page>(),
        new PageSourceProvider<ObservableRecipient1Page>(),
        new PageSourceProvider<RelayCommand1Page>(),
        
    };
}
