using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Observables;

// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/observableproperty#notifying-dependent-properties
public partial class ObservableProperty2Page : MyPageControlBase
{
    public ObservableProperty2Page()
    {
        DataContext = new ObserveProperty2ViewModel();
        InitializeComponent();
    }
}

//[INotifyPropertyChanged]  // Do not use as much as possible (because of increase file size)
internal sealed partial class ObserveProperty2ViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Answer))]
    private int _value1;

    [RelayCommand]
    private void CountUp1() => Value1++;

    public int Answer => Value1 * 3;
}
