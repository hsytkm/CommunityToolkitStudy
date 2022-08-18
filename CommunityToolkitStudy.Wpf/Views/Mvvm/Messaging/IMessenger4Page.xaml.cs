using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
public sealed partial class IMessenger4Page : MyPageControlBase
{
    public IMessenger4Page()
    {
        DataContext = new IMessenger4ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class IMessenger4ViewModel
    : ObservableRecipient    // IsActive に必要です
    , IRecipient<PropertyChangedMessage<TimeOnly>>      // IMessenger3Page からの変化点
{
    // VMを直接参照しません
    readonly IMessenger4Model _model = new();

    [ObservableProperty]
    string _latestTime = "";

    public IMessenger4ViewModel() => IsActive = true;

    [RelayCommand]
    void RequestCurrentTime()
    {
        // 1. 宛先は不明で情報更新の要求メッセージ(専用型)を送出します。疎結合
        _ = Messenger.Send(RequestLatestInfoUnitMessage.Shared);
    }

    [RelayCommand]
    void ClearTime() => LatestTime = "";

    public void Receive(PropertyChangedMessage<TimeOnly> message)
    {
        // 3. 差出元は不明で(型とプロパティ名から判定で)メッセージを監視して値を取得します。疎結合
        LatestTime = message.NewValue.ToString($"HH:mm:ss.fff");
    }
}

internal sealed partial class IMessenger4Model : ObservableRecipient
{
    public IMessenger4Model() => IsActive = true;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]   // Message.Send<PropertyChangedMessage<T>>(message)
    TimeOnly _updateTime;

    protected override void OnActivated()
    {
        Messenger.Register<IMessenger4Model, RequestLatestInfoUnitMessage>(this, static (r, m) =>
        {
            // 2. 差出元は不明で(メッセージ型から判定で)メッセージが来たら、データ(現在時刻)を更新します。疎結合
            r.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);
        });
    }
}
