using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Input;

// [RelayCommand 属性 - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/relaycommand)
// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/observableproperty#notifying-dependent-commands
public sealed partial class AsyncRelayCommand1Page : MyPageControlBase
{
    public AsyncRelayCommand1Page()
    {
        DataContext = new AsyncRelayCommand1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class AsyncRelayCommand1ViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Answer))]
    [NotifyCanExecuteChangedFor(nameof(CountUp2Command))]   // Update CanExecute()
    private int _value1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Answer))]
    private int _value2;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Answer))]
    private int _value3;

    // generate CountUp1Command
    [RelayCommand]  // IAsyncRelayCommand
    private async Task CountUp1Async()  // ValueTask は使用できません
    {
        // https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/relaycommand#asynchronous-commands
        await Task.Delay(500);
        Value1++;
    }

    // generate CountUp2Command and CountUp2CancelCommand
    [RelayCommand(CanExecute = nameof(CanCountUp2), IncludeCancelCommand = true)]    // IAsyncRelayCommand<int>
    private async Task CountUp2Async(int value, CancellationToken token)
    {
        // https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/relaycommand#cancel-commands-for-asynchronous-operations
        // (IncludeCancelCommand = true) を付けると、XXXCancelCommand が生成されます。
        // CancellationToken の指定が必要になります。
        try
        {
            await Task.Delay(1500, token);
            Value2 += value;
        }
        catch (TaskCanceledException) { }
    }
    private bool CanCountUp2() => (Value1 & 1) == 1;  // odd only

    // generate CountUp3Command
    [RelayCommand(AllowConcurrentExecutions = true)]
    private async Task CountUp3Async(int value)
    {
        // https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/relaycommand#handling-concurrent-executions
        // (AllowConcurrentExecutions = true) を付けると、
        // 非同期処理の実行中も CanExecute が false にならず、同時実行を許可できます。
        // CancellationToken が指定されていたら 元の Token が取り消されます。
        await Task.Delay(2000);
        Value3 += value;
    }

    public int Answer => Value1 + Value2 + Value3;

    // 非同期の例外 (FlowExceptionsToTaskScheduler)
    // https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/relaycommand#asynchronous-commands
    // 既定(false)は同期コンテキストで例外が自然にスローされます。
    // true だと タスクスケジューラ に例外のフローされて、アプリがクラッシュしなくなります。
    // ◆タスクスケジューラ例外を理解度できてないのでサンプルを作れていません...
}
