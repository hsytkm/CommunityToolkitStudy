using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
// [ValueChangedMessage<T> Class (Microsoft.Toolkit.Mvvm.Messaging.Messages) | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.messages.valuechangedmessage-1)
public sealed partial class ValueChangedMessage1Page : MyPageControlBase
{
    public ValueChangedMessage1Page()
    {
        DataContext = new ValueChangedMessage1ViewModel();
        InitializeComponent();
    }
}

// ボタンクリック数のメッセージ
internal sealed class ClickedCounterChangedMessage : ValueChangedMessage<int>
{
    internal ClickedCounterChangedMessage(int value) : base(value) { }
}

internal sealed /*partial*/ class ValueChangedMessage1ViewModel : ObservableObject
{
    public ValueChangedMessage1_SenderViewModel SenderViewModel { get; } = new();
    public ValueChangedMessage1_ReceiverViewModel ReceiverViewModel { get; } = new();
}

internal sealed partial class ValueChangedMessage1_SenderViewModel : ObservableObject
{
    int _counter;

    [RelayCommand]
    void Click()
    {
        _counter++;

        // 1. 宛先は不明で情報更新の要求メッセージ(専用型)を送出します。疎結合
        _ = WeakReferenceMessenger.Default.Send(new ClickedCounterChangedMessage(_counter));
    }
}

internal sealed partial class ValueChangedMessage1_ReceiverViewModel
    : ObservableRecipient    // IsActive に必要です
{
    public ValueChangedMessage1_ReceiverViewModel() => IsActive = true;

    [ObservableProperty]
    string _textLog = "";

    protected override void OnActivated()
    {
        Messenger.Register<ValueChangedMessage1_ReceiverViewModel, ClickedCounterChangedMessage>(this, static (r, m) =>
        {
            // 2. 差出元は不明で(メッセージ型から判定で)メッセージが来たら、データ(現在時刻)を更新します。疎結合
            r.TextLog = $"Clicked Counter : {m.Value}";
        });
    }
}
