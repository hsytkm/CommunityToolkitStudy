using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
// [AsyncCollectionRequestMessage<T> Class (Microsoft.Toolkit.Mvvm.Messaging.Messages) | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/api/microsoft.toolkit.mvvm.messaging.messages.asynccollectionrequestmessage-1)
public sealed partial class AsyncCollectionRequestMessage1Page : MyPageControlBase
{
    public AsyncCollectionRequestMessage1Page()
    {
        DataContext = new AsyncCollectionRequestMessage1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class AsyncCollectionRequestMessage1ViewModel : ObservableObject, IDisposable
{
    sealed class RandomNumber1AsyncCollectionRequestMessage : AsyncCollectionRequestMessage<int> { }

    sealed /*partial*/ class TimePublisherViewModel : ObservableRecipient, IDisposable
    {
        public TimePublisherViewModel() => IsActive = true;

        // called when IsActive changes to true.
        protected override void OnActivated()
        {
            Messenger.Register<RandomNumber1AsyncCollectionRequestMessage>(this, static (r, m) =>
            {
                static int GetRandomValue() => Random.Shared.Next(1, 11);
                static async Task<int> GetRandomValueAsync()
                {
                    await Task.Delay(1000).ConfigureAwait(false);
                    return GetRandomValue();
                }

                // 1.結果のコレクションを 非同期で 返します。
                var count = GetRandomValue();
                for (int i = 0; i < count; i++)
                {
                    var task = GetRandomValueAsync();
                    m.Reply(task);
                }

                // 奇数なら値を追加 (Task<T>以外も登録できますよの例)
                if ((count & 1) != 0)
                    m.Reply(0);
            });
        }

        // IsActive=false にしないと、WeakReference が(すぐに?)解除されないので Exception が発生します。
        public void Dispose() => IsActive = false;
    }

    // VMを直接参照しません。(メッセージで応答してもらいます)
    readonly TimePublisherViewModel _clockViewModel = new();

    [ObservableProperty]
    string _textLog = "";

    [RelayCommand]
    async Task RequestValues()
    {
        // 宛先不明でメッセージを投げて返答を取得します
        var message = WeakReferenceMessenger.Default.Send<RandomNumber1AsyncCollectionRequestMessage>();

        // 2.結果のコレクションを 非同期で 取得します。
        var items = await message.GetResponsesAsync(CancellationToken.None);
        TextLog = string.Join(", ", items);
    }

    [RelayCommand]
    void ClearTextLog() => TextLog = "";

    public void Dispose() => _clockViewModel.Dispose();
}
