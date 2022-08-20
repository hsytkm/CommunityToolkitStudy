using System.Diagnostics.CodeAnalysis;
using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
// [StrongReferenceMessenger Class (Microsoft.Toolkit.Mvvm.Messaging) | Microsoft Docs](https://docs.microsoft.com/ja-JP/dotnet/api/microsoft.toolkit.mvvm.Messaging.StrongReferenceMessenger)
public sealed partial class StrongReferenceMessenger1Page : MyPageControlBase
{
    public StrongReferenceMessenger1Page()
    {
        DataContext = new StrongReferenceMessenger1ViewModel();
        InitializeComponent();
    }
}

internal interface ICounter
{
    int Counter { get; }
    int CountUp();
}

// 下記を Strong/WeakReference で行います。
//   1. ParentVM が Strong(Weak)ReferenceMessenger でメッセージを配信
//   2. ChildVM がメッセージを受信して Model.CountUp() を実行
//   3. ParentVM が Model の値を参照
//
//  ⇒ Strong/Weak の動作の違いを示したかのですが、示せませんでした。
internal sealed partial class StrongReferenceMessenger1ViewModel : ObservableRecipient
{
    sealed partial class MyCounter : ObservableRecipient, ICounter
    {
        [ObservableProperty]
        [NotifyPropertyChangedRecipients]
        int _counter;
        public int CountUp() => Counter++;
    }

    readonly MyCounter _strongCounter = new();
    readonly MyCounter _weakCounter = new();

    StrongReferenceMessenger1_StrongReceiverViewModel _strongReceiverViewModel { get; set; }
    StrongReferenceMessenger1_WeakReceiverViewModel _weakReceiverViewModel { get; set; }

    [ObservableProperty] Guid _strongGuid;
    [ObservableProperty] Guid _weakGuid;
    [ObservableProperty] string _text1 = "";
    [ObservableProperty] string _text2 = "";

    public StrongReferenceMessenger1ViewModel()
    {
        ReplaceInstance_Strong();
        ReplaceInstance_Weak();
        IsActive = true;
    }

    [RelayCommand]
    [MemberNotNull(nameof(_strongReceiverViewModel))]
    void ReplaceInstance_Strong()
    {
        // StrongReferenceMessenger を使用しているので、明示的に Receiver を解除する必要があります。
        // 明示的に !IsActive にして OnDeactivated() を実行させます。
        if (_strongReceiverViewModel is { } vm)
            vm.IsActive = false;

        _strongReceiverViewModel = new(_strongCounter);
        StrongGuid = _strongReceiverViewModel.Guid;
    }

    [RelayCommand]
    [MemberNotNull(nameof(_weakReceiverViewModel))]
    void ReplaceInstance_Weak()
    {
        // WeakReferenceMessenger を使用している場合は、明示的に Receiver を解除する必要がありません。
        // が、OnDeactivated() を実行させないと解除されませんでした。なぜ？◆
        if (_weakReceiverViewModel is { } vm)
            vm.IsActive = false;

        _weakReceiverViewModel = new(_weakCounter);
        WeakGuid = _weakReceiverViewModel.Guid;
    }

    [RelayCommand]
    void RequestCountUp_Strong()
    {
        StrongReferenceMessenger.Default.Send(RequestCountUpUnitMessage.Shared);
        Text1 = $"Strong : {_strongCounter.Counter}";
    }

    [RelayCommand]
    void RequestCountUp_Weak()
    {
        WeakReferenceMessenger.Default.Send(RequestCountUpUnitMessage.Shared);
        Text2 = $"Weak : {_weakCounter.Counter}";
    }
}

// BaseClass
internal abstract /*partial*/ class StrongReferenceMessenger1_ReceiverBase
    : ObservableRecipient, IRecipient<RequestCountUpUnitMessage>
{
    public Guid Guid { get; } = Guid.NewGuid();
    readonly ICounter _counter;

    public StrongReferenceMessenger1_ReceiverBase(IMessenger messenger, ICounter counter)
        : base(messenger)   // 未指定だと WeakReferenceMessenger になります
    {
        _counter = counter;
        IsActive = true;
    }

    public void Receive(RequestCountUpUnitMessage message) => _counter.CountUp();
}

// Use StrongReferenceMessenger
internal sealed /*partial*/ class StrongReferenceMessenger1_StrongReceiverViewModel
    : StrongReferenceMessenger1_ReceiverBase
{
    public StrongReferenceMessenger1_StrongReceiverViewModel(ICounter counter)
        : base(StrongReferenceMessenger.Default, counter)
    { }
}

// Use WeakReferenceMessenger
internal sealed /*partial*/ class StrongReferenceMessenger1_WeakReceiverViewModel
    : StrongReferenceMessenger1_ReceiverBase
{
    public StrongReferenceMessenger1_WeakReceiverViewModel(ICounter counter)
        : base(WeakReferenceMessenger.Default, counter)
    { }
}
