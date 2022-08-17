using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkitStudy.Wpf.Views.Controls;
using static CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging.IMessenger2Page;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
public sealed partial class IMessenger2Page : MyPageControlBase
{
    // Message の Token(channel) には IEquatable<T> が必要なので Enum は使用不可
    public readonly record struct TimeZoneId(int Id)
    {
        public static readonly TimeZoneId Utc = new(0);
        public static readonly TimeZoneId Local = new(1);
    }

    public IMessenger2Page()
    {
        DataContext = new IMessenger2ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class IMessenger2ViewModel : ObservableObject, IDisposable
{
    sealed class CurrentTime2RequestMessage : RequestMessage<TimeOnly> { }

    sealed /*partial*/ class TimePublisher2ViewModel : ObservableRecipient, IDisposable
    {
        public TimePublisher2ViewModel() => IsActive = true;

        // called when IsActive changes to true.
        protected override void OnActivated()
        {
            // Token(channel) を指定した登録
            Messenger.Register<CurrentTime2RequestMessage, TimeZoneId>(this, TimeZoneId.Utc,
                static (r, m) =>   // may be r=receiver, m=message
                {
                    var timeNow = TimeOnly.FromDateTime(DateTime.UtcNow);
                    m.Reply(timeNow);
                });

            Messenger.Register<CurrentTime2RequestMessage, TimeZoneId>(this, TimeZoneId.Local,
                static (r, m) =>   // may be r=receiver, m=message
                {
                    var timeNow = TimeOnly.FromDateTime(DateTime.Now);
                    m.Reply(timeNow);
                });
        }

        // called when IsActive changes to false.
        protected override void OnDeactivated()
        {
            // Messenger の Token ごとに解除が必要です
            //Messenger.Unregister<CurrentTime2RequestMessage, TimeZoneId>(this, TimeZoneId.Utc);
            //Messenger.Unregister<CurrentTime2RequestMessage, TimeZoneId>(this, TimeZoneId.Local);

            // 全解除メソッドが存在します
            Messenger.UnregisterAll(this);
        }

        public void Dispose() => IsActive = false;
    }

    // VMを直接参照しません。(メッセージで応答してもらいます)
    readonly TimePublisher2ViewModel _clockViewModel = new();

    [ObservableProperty]
    string _currentTime = "";

    void RequestCurrentTime(TimeZoneId timeZone)
    {
        // 宛先不明でメッセージを投げて返答(現在時刻)を取得します
        // Token(channel) を指定して発信します
        var message = WeakReferenceMessenger.Default.Send<CurrentTime2RequestMessage, TimeZoneId>(timeZone);
        CurrentTime = message.HasReceivedResponse ? message.Response.ToString($"HH:mm:ss.fff") : "";
    }

    [RelayCommand]
    void RequestCurrentUtcTime() => RequestCurrentTime(TimeZoneId.Utc);  // Token指定あり

    [RelayCommand]
    void RequestCurrentLocalTime() => RequestCurrentTime(TimeZoneId.Local);  // Token指定あり

    [RelayCommand]
    void ClearTime() => CurrentTime = "";

    public void Dispose() => _clockViewModel.Dispose();
}
