using CommunityToolkitStudy.Wpf.Services;
using CommunityToolkitStudy.Wpf.Views.Mvvm.ComponentModel;
using CommunityToolkitStudy.Wpf.Views.Mvvm.DependencyInjection;
using CommunityToolkitStudy.Wpf.Views.Mvvm.Input;
using CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

namespace CommunityToolkitStudy.Wpf.Views;

internal static class PageSourceStore
{
    // [Community Toolkits のドキュメント - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/)
    internal static IReadOnlyList<IPageSourceProvider> All { get; } = new IPageSourceProvider[]
    {
        /*
         * WeakReferenceMessenger
         * StrongReferenceMessenger     手動での解除が必要ですが、メモリ使用量が少なくパフォーマンス良いです
         */
        //new PageSourceProvider<Page>(),

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
        new PageSourceProvider<RequestMessage1Page>(),
        new PageSourceProvider<RequestMessage2Page>(),
        new PageSourceProvider<AsyncRequestMessage1Page>(),
        new PageSourceProvider<ValueChangedMessage1Page>(),
        new PageSourceProvider<PropertyChangedMessage1Page>(),
        new PageSourceProvider<IRecipient1Page>(),
        new PageSourceProvider<CollectionRequestMessage1Page>(),
        new PageSourceProvider<AsyncCollectionRequestMessage1Page>(),

    };

    // [Introduction to the High Performance package - Windows Community Toolkit | Microsoft Docs] (https://docs.microsoft.com/ja-jp/windows/communitytoolkit/high-performance/introduction)
    // [Introduction to the Diagnostics package - Windows Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/windows/communitytoolkit/diagnostics/introduction)

    /* 以下は UWP(XF?)コントロール用 っぽいです。
     *  [Observable groups - Windows Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/windows/communitytoolkit/collections/observablegroups)
     *  
     *  - ObservableGroup<TKey, TElement>
     *  - ObservableGroupedCollection<TKey, TElement>
     *  - ReadOnlyObservableGroup<TKey, TElement>
     *  - ReadOnlyObservableGroupedCollection<TKey, TElement>
     *
     *  WPF の CollectionViewSource と異なっており、使えなさげでした。
     *    WPF: System.Windows.Data.CollectionViewSource
     *    UWP: Windows.UI.Xaml.Data.CollectionViewSource
     */
}
