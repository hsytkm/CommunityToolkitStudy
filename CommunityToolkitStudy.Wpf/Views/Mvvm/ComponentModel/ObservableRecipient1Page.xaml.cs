using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.ComponentModel;

// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/observableproperty#sending-notification-messages
// [ObservableRecipient - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/observablerecipient)
public sealed partial class ObservableRecipient1Page : MyPageControlBase
{
    public ObservableRecipient1Page()
    {
        DataContext = new ObservableRecipient1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class ObservableRecipient1ViewModel : ObservableObject
{
    public ObservableRecipient1_SenderViewModel SenderViewModel { get; } = new();
    public ObservableRecipient1_Recipient1ViewModel Recipient1ViewModel { get; } = new();
    public ObservableRecipient1_Recipient2ViewModel Recipient2ViewModel { get; } = new();
}

// ObservableRecipient は メッセージの送信者も継承します。
// フィールドに [NotifyPropertyChangedRecipients] を付与すると、Broadcast<T> で Messenger.Send(message) が行われます。
// 受信側は 型 と プロパティ名 でメッセージを処理しますので、実際の環境では 専用クラス をSendしないと扱い難いと思います。
internal sealed partial class ObservableRecipient1_SenderViewModel : ObservableRecipient
{
    public string Name { get; } = "Sender";

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]   // ObservableRecipient の継承が必要です
    private string _text1 = "text1";

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]   // Message.Send<PropertyChangedMessage<T>>(message)
    private string _text2 = "text2";
}

// ObservableRecipient は メッセージの受信者も継承します。
// IRecipient<T> を実装しない場合、OnActivated() にて自分でメッセージを処理する必要があります。
internal sealed /*partial*/ class ObservableRecipient1_Recipient1ViewModel : ObservableRecipient
{
    public string Name { get; } = "Receiver1";
    public ObservableCollection<string> Texts { get; } = new();

    public ObservableRecipient1_Recipient1ViewModel() => IsActive = true;

    // register myself
    protected override void OnActivated()
    {
        Messenger.Register<ObservableRecipient1_Recipient1ViewModel, PropertyChangedMessage<string>>(this, static (r, m) =>
        {
            var message = m.PropertyName switch
            {
                nameof(ObservableRecipient1_SenderViewModel.Text1) => $"{m.PropertyName} : {m.OldValue} -> {m.NewValue}",
                _ => $"{m.PropertyName} : Ignored"
            };
            r.Texts.Add(message);
        });
    }
}

// ObservableRecipient は メッセージの受信者も継承します。
// IRecipient<T> を実装すると、Receive(T) にメッセージが通知されて便利です。
internal sealed /*partial*/ class ObservableRecipient1_Recipient2ViewModel
    : ObservableRecipient, IRecipient<PropertyChangedMessage<string>>
{
    public string Name { get; } = "Receiver2";
    public ObservableCollection<string> Texts { get; } = new();

    public ObservableRecipient1_Recipient2ViewModel() => IsActive = false;

    // register automatically
    public void Receive(PropertyChangedMessage<string> message) =>
        Texts.Add($"{message.PropertyName} : {message.OldValue} -> {message.NewValue}");
}
