using System.Collections.Generic;
using CommunityToolkitStudy.Wpf.Services;
using CommunityToolkitStudy.Wpf.Views.Observables;

namespace CommunityToolkitStudy.Wpf.Views;

internal static class PageSourceStore
{
    internal static IReadOnlyList<IPageSourceProvider> AllPageList => new IPageSourceProvider[]
    {
        /* 未対応
        *  CatchIgnore ErrorChangedAsObservable 
        *  ObserveErrorInfo OnErrorRetry
        */
        new PageSourceProvider<ObservableProperty1Page>(),
    };
}
