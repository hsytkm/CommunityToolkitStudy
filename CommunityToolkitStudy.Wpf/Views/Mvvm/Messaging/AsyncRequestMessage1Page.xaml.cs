using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
// [AsyncRequestMessage<T> Class (Microsoft.Toolkit.Mvvm.Messaging.Messages) | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.messages.asyncrequestmessage-1)
public sealed partial class AsyncRequestMessage1Page : MyPageControlBase
{
    public AsyncRequestMessage1Page()
    {
        DataContext = new AsyncRequestMessage1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class AsyncRequestMessage1ViewModel : ObservableObject, IDisposable
{
    // 現在時刻を要求するメッセージです
    sealed class CurrentTime1AsyncRequestMessage : AsyncRequestMessage<TimeOnly> { }

    sealed /*partial*/ class TimePublisherViewModel : ObservableRecipient, IDisposable
    {
        public TimePublisherViewModel() => IsActive = true;

        // called when IsActive changes to true.
        protected override void OnActivated()
        {
            Messenger.Register<CurrentTime1AsyncRequestMessage>(this, static (r, m) =>
            {
                // 1. 結果を Task<T> で返します。
                static async Task<TimeOnly> GetCurrentTimeAsync()
                {
                    await Task.Delay(1000).ConfigureAwait(false);
                    return TimeOnly.FromDateTime(DateTime.Now);
                }
                var task = GetCurrentTimeAsync();
                m.Reply(task);
            });
        }

        // IsActive=false にしないと、WeakReference が(すぐに?)解除されないので Exception が発生します。
        public void Dispose() => IsActive = false;
    }

    // VMを直接参照しません。(メッセージで応答してもらいます)
    readonly TimePublisherViewModel _clockViewModel = new();

    [ObservableProperty]
    string _currentTime = "";

    [RelayCommand]
    async Task RequestCurrentTime()
    {
        // 宛先不明でメッセージを投げて返答(現在時刻)を取得します
        var message = WeakReferenceMessenger.Default.Send<CurrentTime1AsyncRequestMessage>();
        if (message.HasReceivedResponse)
        {
            // 2.Task<T> から結果を取得します。
            var value = await message.Response;
            CurrentTime = value.ToString($"HH:mm:ss.fff");
        }
    }

    [RelayCommand]
    void ClearTime() => CurrentTime = "";

    public void Dispose() => _clockViewModel.Dispose();
}
