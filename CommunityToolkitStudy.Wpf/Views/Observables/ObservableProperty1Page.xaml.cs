using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Observables;

public partial class ObservableProperty1Page : MyPageControlBase
{
    public ObservableProperty1Page()
    {
        DataContext = new ObserveProperty1ViewModel();
        InitializeComponent();
    }
}

//[INotifyPropertyChanged]  極力使用しない(ファイルサイズ増)
internal sealed partial class ObserveProperty1ViewModel : ObservableObject
{
    public int CounterSum => Counter1 + Counter2;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CounterSum))]
    [NotifyCanExecuteChangedFor(nameof(CountUp2Command))]
    private int _counter1;

    [RelayCommand]
    private void CountUp1() => Counter1++;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CounterSum))]
    private int _counter2;

    [RelayCommand(CanExecute = nameof(CanCountUp2))]
    private void CountUp2(int value) => Counter2 += value;
    private bool CanCountUp2() => (Counter1 & 1) == 1;  // 奇数のみ有効化
}
