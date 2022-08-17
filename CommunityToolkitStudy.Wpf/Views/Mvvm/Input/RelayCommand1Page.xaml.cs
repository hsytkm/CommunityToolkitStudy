using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Input;

// [RelayCommand 属性 - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/relaycommand)
// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/observableproperty#notifying-dependent-commands
public sealed partial class RelayCommand1Page : MyPageControlBase
{
    public RelayCommand1Page()
    {
        DataContext = new RelayCommand1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class RelayCommand1ViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Answer))]
    [NotifyCanExecuteChangedFor(nameof(CountUp2Command))]   // Update CanExecute()
    int _value1;

    // generate CountUp1Command
    [RelayCommand]  // IRelayCommand
    void CountUp1() => Value1++;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Answer))]
    int _value2;

    // generate CountUp2Command and CountUp2CancelCommand
    [RelayCommand(CanExecute = nameof(CanCountUp2))]    // IRelayCommand<int>
    void CountUp2(int value)    // CommandParameter
    {
        Value2 += value;
    }

    bool CanCountUp2()  // CanExecute()
    {
        return (Value1 & 1) == 1;  // odd only
    }

    public int Answer => Value1 + Value2;
}
