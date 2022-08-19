using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
// [RequestMessage<T> Class (Microsoft.Toolkit.Mvvm.Messaging.Messages) | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.messages.requestmessage-1)
public sealed partial class RequestMessage1Page : MyPageControlBase
{
    public RequestMessage1Page()
    {
        DataContext = new RequestMessage1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class RequestMessage1ViewModel : ObservableObject, IDisposable
{
    // 現在時刻を要求するメッセージです
    sealed class CurrentTime1RequestMessage : RequestMessage<TimeOnly> { }

    sealed /*partial*/ class TimePublisherViewModel : ObservableRecipient, IDisposable
    {
        public TimePublisherViewModel() => IsActive = true;

        // called when IsActive changes to true.
        protected override void OnActivated()
        {
            Messenger.Register<CurrentTime1RequestMessage>(this, static (r, m) =>
            {
                // r=recipient, m=message
                var timeNow = TimeOnly.FromDateTime(DateTime.Now);
                m.Reply(timeNow);
            });
        }

#if false   // WeakReferenceMessenger なので自分で解除しなくても良さげ。
        // called when IsActive changes to false.
        protected override void OnDeactivated()
        {
            Messenger.Unregister<CurrentTime1RequestMessage>(this);
            //Messenger.UnregisterAll(this);    面倒なら全解除でOK
        }
#endif
        // IsActive=false にしないと、WeakReference が(すぐに?)解除されないので Exception が発生します。
        public void Dispose() => IsActive = false;
    }

    // VMを直接参照しません。(メッセージで応答してもらいます)
    readonly TimePublisherViewModel _clockViewModel = new();

    [ObservableProperty]
    string _currentTime = "";

    [RelayCommand]
    void RequestCurrentTime()
    {
        // 宛先不明でメッセージを投げて返答(現在時刻)を取得します
        var message = WeakReferenceMessenger.Default.Send<CurrentTime1RequestMessage>();
        CurrentTime = message.HasReceivedResponse ? message.Response.ToString($"HH:mm:ss.fff") : "";
    }

    [RelayCommand]
    void ClearTime() => CurrentTime = "";

    public void Dispose() => _clockViewModel.Dispose();
}
