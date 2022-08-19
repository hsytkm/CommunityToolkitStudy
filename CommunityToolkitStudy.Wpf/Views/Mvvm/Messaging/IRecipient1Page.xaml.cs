using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
// [IRecipient<TMessage> Interface (Microsoft.Toolkit.Mvvm.Messaging) | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.irecipient-1)
public sealed partial class IRecipient1Page : MyPageControlBase
{
    public IRecipient1Page()
    {
        DataContext = new IRecipient1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class IRecipient1ViewModel
    : ObservableRecipient                           // IsActive に必要です
    , IRecipient<PropertyChangedMessage<TimeOnly>>  // PropertyChangedMessage1Page からの変化点
{
    readonly ClockModel _clockModel = new();

    [ObservableProperty]
    string _latestTime = "";

    public IRecipient1ViewModel() => IsActive = true;

    [RelayCommand]
    void RequestCurrentTime()
    {
        // 1. UI操作から Model.Method() を実行します。疎結合ではない
        _clockModel.Update();
    }

    [RelayCommand]
    void ClearTime() => LatestTime = "";

    // RegisterAll() で登録されるそうです。 docsより
    public void Receive(PropertyChangedMessage<TimeOnly> message)
    {
        // 3. 差出元は不明で(型とプロパティ名から判定で)メッセージを監視して値を取得します。疎結合
        LatestTime = message.NewValue.ToString($"HH:mm:ss.fff");
    }
}
