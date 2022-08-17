using CommunityToolkitStudy.Wpf.Views.Controls;

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

// 情報の更新を要求します(ジェネリック値は使用していません)
internal sealed class RequestLatestInfomationChangedMessage : ValueChangedMessage<object?>
{
    internal static RequestLatestInfomationChangedMessage Shared { get; } = new();
    internal RequestLatestInfomationChangedMessage(object? value = null) : base(value) { }
}

internal sealed partial class IMessenger3ViewModel
    : ObservableRecipient    // OnActivated() に必要です
{
    // VMを直接参照しません
    readonly IMessenger3RecieveSenderViewModel _recieveViewModel = new();

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
                case nameof(IMessenger3RecieveSenderViewModel.UpdateTime):
                    r.LatestTime = m.NewValue.ToString($"HH:mm:ss.fff");
                    break;
            };
        });
    }

    [RelayCommand]
    static void RequestCurrentTime()
    {
        // 1. 宛先は不明で情報更新の要求メッセージ(専用型)を送出します。疎結合
        _ = WeakReferenceMessenger.Default.Send(RequestLatestInfomationChangedMessage.Shared);
    }

    [RelayCommand]
    void ClearTime() => LatestTime = "";
}

internal sealed partial class IMessenger3RecieveSenderViewModel : ObservableRecipient
{
    public IMessenger3RecieveSenderViewModel() => IsActive = true;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]   // Message.Send<PropertyChangedMessage<T>>(message)
    TimeOnly _updateTime;

    protected override void OnActivated()
    {
        Messenger.Register<IMessenger3RecieveSenderViewModel, RequestLatestInfomationChangedMessage>(this, static (r, m) =>
        {
            // 2. 差出元は不明で(メッセージ型から判定で)メッセージが来たら、データ(現在時刻)を更新します。疎結合
            r.UpdateTime = TimeOnly.FromDateTime(DateTime.Now);
        });
    }
}
