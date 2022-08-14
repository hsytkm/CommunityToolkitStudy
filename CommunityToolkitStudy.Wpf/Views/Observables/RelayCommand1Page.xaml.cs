using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Observables;

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
    [NotifyCanExecuteChangedFor(nameof(CountUp2Command))]
    private int _value1;

    [RelayCommand]
    private void CountUp1() => Value1++;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Answer))]
    private int _value2;
    
    [RelayCommand(CanExecute = nameof(CanCountUp2))]
    private void CountUp2(int value) => Value2 += value;
    private bool CanCountUp2() => (Value1 & 1) == 1;  // 奇数のみ有効化

    public int Answer => Value1 + Value2;
}
