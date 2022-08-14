using System.Collections.ObjectModel;
using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.ComponentModel;

// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/observableproperty
public sealed partial class ObservableProperty1Page : MyPageControlBase
{
    public ObservableProperty1Page()
    {
        DataContext = new ObserveProperty1ViewModel();
        InitializeComponent();
    }
}

//[INotifyPropertyChanged]  // Do not use as much as possible (because of increase file size)
internal sealed partial class ObserveProperty1ViewModel : ObservableObject
{
    [ObservableProperty]    // "Name" property is generated in the partial class.
    private string _name = "abc";

    public ObservableCollection<string> Messages { get; } = new();

    // Called before value is set.
    partial void OnNameChanging(string value)   // Argument name is "value"
    {
        Messages.Add($"{nameof(OnNameChanging)}({value})");
    }

    // Called after value is set.
    partial void OnNameChanged(string value)   // Argument name is "value"
    {
        Messages.Add($"{nameof(OnNameChanged)}({value})");
    }
}
