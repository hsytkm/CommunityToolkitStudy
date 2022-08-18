using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
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
    private sealed class CurrentTime1RequestMessage : RequestMessage<TimeOnly> { }

    private sealed /*partial*/ class TimePublisherViewModel : ObservableRecipient, IDisposable
    {
        public TimePublisherViewModel() => IsActive = true;

        // called when IsActive changes to true.
        protected override void OnActivated()
        {
            Messenger.Register<CurrentTime1RequestMessage>(this,
                static (r, m) =>   // may be r=receiver, m=message
                {
                    var timeNow = TimeOnly.FromDateTime(DateTime.Now);
                    m.Reply(timeNow);
                });
        }

        // called when IsActive changes to false.
        protected override void OnDeactivated()
        {
            Messenger.Unregister<CurrentTime1RequestMessage>(this);
            //Messenger.UnregisterAll(this);    面倒なら全解除でOK
        }

        public void Dispose() => IsActive = false;
    }

    // VMを直接参照しません。(メッセージで応答してもらいます)
    private readonly TimePublisherViewModel _clockViewModel = new();

    [ObservableProperty]
    private string _currentTime = "";

    [RelayCommand]
    private void RequestCurrentTime()
    {
        // 宛先不明でメッセージを投げて返答(現在時刻)を取得します
        var message = WeakReferenceMessenger.Default.Send<CurrentTime1RequestMessage>();
        CurrentTime = message.HasReceivedResponse ? message.Response.ToString($"HH:mm:ss.fff") : "";
    }

    [RelayCommand]
    void ClearTime() => CurrentTime = "";

    public void Dispose() => _clockViewModel.Dispose();
}
