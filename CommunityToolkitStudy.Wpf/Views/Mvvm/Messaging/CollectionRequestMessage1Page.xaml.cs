using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
public sealed partial class CollectionRequestMessage1Page : MyPageControlBase
{
    public CollectionRequestMessage1Page()
    {
        DataContext = new CollectionRequestMessage1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class CollectionRequestMessage1ViewModel : ObservableObject, IDisposable
{
    sealed class RandomNumber1CollectionRequestMessage : CollectionRequestMessage<int> { }

    sealed /*partial*/ class TimePublisherViewModel : ObservableRecipient, IDisposable
    {
        public TimePublisherViewModel() => IsActive = true;

        // called when IsActive changes to true.
        protected override void OnActivated()
        {
            Messenger.Register<RandomNumber1CollectionRequestMessage>(this, static (r, m) =>
            {
                static int GetRandomValue() => Random.Shared.Next(1, 11);

                // 1.結果のコレクションを返します。
                for (int i = 0; i < GetRandomValue(); i++)
                {
                    m.Reply(GetRandomValue());
                }
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
    void RequestValues()
    {
        // 宛先不明でメッセージを投げて返答を取得します
        var message = WeakReferenceMessenger.Default.Send<RandomNumber1CollectionRequestMessage>();

        // CollectionRequestMessage.HasReceivedResponse は存在しません

        // 2.結果のコレクションを取得します。
        TextLog = string.Join(", ", message);
    }

    [RelayCommand]
    void ClearTextLog() => TextLog = "";

    public void Dispose() => _clockViewModel.Dispose();
}
