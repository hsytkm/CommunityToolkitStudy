using System.Collections.ObjectModel;
using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.ComponentModel;

// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/inotifypropertychanged
public sealed partial class INotifyPropertyChanged1Page : MyPageControlBase
{
    public INotifyPropertyChanged1Page()
    {
        DataContext = new INotifyPropertyChanged1ViewModel();
        InitializeComponent();
    }
}

internal abstract class MyClassBase
{
    protected virtual string Header { get; } = "foo : ";
}

// ファイルサイズが増えるので特定の継承が必要なければ ObservableObject を継承しましょう。
// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/inotifypropertychanged
[INotifyPropertyChanged]
internal sealed partial class INotifyPropertyChanged1ViewModel : MyClassBase
{
    [ObservableProperty]
    private string _name = "abc";

    public ObservableCollection<string> Messages { get; } = new();

    partial void OnNameChanged(string value) => Messages.Add(Header + value);
}
