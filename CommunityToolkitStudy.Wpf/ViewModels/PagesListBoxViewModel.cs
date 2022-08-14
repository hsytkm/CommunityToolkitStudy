using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkitStudy.Wpf.Services;
using CommunityToolkitStudy.Wpf.Views;

namespace CommunityToolkitStudy.Wpf.ViewModels;

internal sealed class PagesListBoxViewModel : ObservableObject
{
    public IReadOnlyList<IPageSourceProvider> PagesSource { get; } = PageSourceStore.All;

    // リストの文字列による絞り込み
    public ICommand FilterCommand => _filterCommand ??= new RelayCommand<string>(pattern =>
    {
        var collectionView = CollectionViewSource.GetDefaultView(PagesSource);

        collectionView.Filter = string.IsNullOrWhiteSpace(pattern) ? null     // clear
            : x => (x as IPageSourceProvider)?.IsContain(pattern) ?? false;

        // フィルタリング後の先頭アイテムに移動させる
        if (collectionView.Cast<IPageSourceProvider>().FirstOrDefault() is { } page)
            SelectedPageSource = page;
    });
    private ICommand? _filterCommand;

    // Viewで選択されたアイテム(こいつをContentに読み込む)
    public IPageSourceProvider? SelectedPageSource
    {
        get => _selectedPageSource;
        set
        {
            // 選択変更でViewコントロールを更新
            if (SetProperty(ref _selectedPageSource, value))
            {
                if (value is not null)
                    TargetControl = value.CreateControl();
            }
        }
    }
    private IPageSourceProvider? _selectedPageSource;

    // Viewに表示するコントロール
    public UserControl? TargetControl
    {
        get => _targetControl;
        private set
        {
            var disposable = _targetControl as IDisposable;
            if (SetProperty(ref _targetControl, value))
                disposable?.Dispose();
        }
    }
    private UserControl? _targetControl;

    public PagesListBoxViewModel()
    {
        SelectedPageSource = PagesSource[0];
    }
}
