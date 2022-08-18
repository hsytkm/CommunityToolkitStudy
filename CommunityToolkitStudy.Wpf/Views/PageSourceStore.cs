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
        /* 
         * AsyncRequestMessage<T>
         * CollectionRequestMessage<T>
         * AsyncCollectionRequestMessage<T>
         * ObservableGroup<TKey, TElement>
         * ObservableGroupedCollection<TKey, TElement>
         * ReadOnlyObservableGroup<TKey, TElement>
         * ReadOnlyObservableGroupedCollection<TKey, TElement>
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
        new PageSourceProvider<ValueChangedMessage1Page>(),
        new PageSourceProvider<PropertyChangedMessage1Page>(),
        new PageSourceProvider<IRecipient1Page>(),


    };
}
