using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.DependencyInjection;

// [Ioc - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/ioc)
public sealed partial class DependencyInjection1Page : MyPageControlBase
{
    public DependencyInjection1Page()
    {
        // VM は コードビハインドで差し込みます。
        //  Prism の ViewModelLocator (勝手に解決) に該当する機能はないようです。
        //  ViewModelLocator の実装を見たところ、アセンブリをリフレクションで動的に解決してそうだったので、
        //  処理不可の観点から推奨してないのかなぁと思いました。（妄想記事です）
        DataContext = App.GetService<DependencyInjection1ViewModel>();
        InitializeComponent();
    }
}

internal sealed partial class DependencyInjection1ViewModel : ObservableObject
{
    readonly DependencyInjection1Model _model;

    // ViewModel の値はページ遷移で引き継がれません。
    [ObservableProperty]
    private int _viewModelValue;

    [RelayCommand]
    private void AddViewModelValue() => ViewModelValue++;

    // Model の値はページ遷移後も引き継がれます。
    [ObservableProperty]
    private int _modelValue;

    [RelayCommand]
    private void AddModelValue() => _model.CountUp();

    // コンストラクタで Model をインジェクションします
    public DependencyInjection1ViewModel(DependencyInjection1Model model)
    {
        _model = model;
        ModelValue = model.Counter;     // Modelの値復帰(ダサい)

        model.PropertyChanged += Model_PropertyChanged;
    }

    // CommunityToolkit でもこの実装しかない？
    private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender is not DependencyInjection1Model model)
            return;

        switch (e.PropertyName)
        {
            case nameof(DependencyInjection1Model.Counter):
                ModelValue = model.Counter;
                break;
        }
    }
}

internal sealed partial class DependencyInjection1Model : ObservableObject
{
    [ObservableProperty]
    private int _counter;

    internal void CountUp() => Counter++;
}
