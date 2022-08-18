﻿using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
public sealed partial class IMessenger3Page : MyPageControlBase
{
    public IMessenger3Page()
    {
        DataContext = new IMessenger3ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class IMessenger3ViewModel
    : ObservableRecipient    // IsActive に必要です
{
    // VMを直接参照しません
    readonly IMessenger3Model _model = new();

    [ObservableProperty]
    string _latestTime = "";

    public IMessenger3ViewModel() => IsActive = true;

    protected override void OnActivated()
    {
        // 3. 差出元は不明で(型とプロパティ名から判定で)メッセージを監視して値を取得します。疎結合
        Messenger.Register<IMessenger3ViewModel, PropertyChangedMessage<TimeOnly>>(this, static (r, m) =>
        {
            switch (m.PropertyName)
            {
                case nameof(IMessenger3Model.UpdateTime):
                    r.LatestTime = m.NewValue.ToString($"HH:mm:ss.fff");
                    break;
            };
        });
    }

    [RelayCommand]
    void RequestCurrentTime()
    {
        // 1. 宛先は不明で情報更新の要求メッセージ(専用型)を送出します。疎結合
        _ = Messenger.Send(RequestLatestInfoUnitMessage.Shared);
    }

    [RelayCommand]
    void ClearTime() => LatestTime = "";
}

internal sealed partial class IMessenger3Model : ObservableRecipient
{
    public IMessenger3Model() => IsActive = true;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]   // Message.Send<PropertyChangedMessage<T>>(message)
    TimeOnly _updateTime;

    protected override void OnActivated()
    {
        Messenger.Register<IMessenger3Model, RequestLatestInfoUnitMessage>(this, static (r, m) =>
        {
            // 2. 差出元は不明で(メッセージ型から判定で)メッセージが来たら、データ(現在時刻)を更新します。疎結合
            r.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);
        });
    }
}
