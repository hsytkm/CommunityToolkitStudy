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
        DataContext = Ioc.Default.GetService<DependencyInjection1ViewModel>();
        InitializeComponent();
    }
}

internal sealed partial class DependencyInjection1ViewModel : ObservableRecipient
{
    readonly DependencyInjection1Model _model;

    // ViewModel の値はページ遷移で引き継がれません。
    [ObservableProperty]
    int _viewModelValue;

    [RelayCommand]
    void AddViewModelValue() => ViewModelValue++;

    // Model の値はページ遷移後も引き継がれます。
    [ObservableProperty]
    int _modelValue;

    [RelayCommand]
    void AddModelValue() => _model.CountUp();

    // コンストラクタで Model をインジェクションします
    public DependencyInjection1ViewModel(DependencyInjection1Model model)
    {
        _model = model;
        ModelValue = model.Counter;     // Modelの値復帰(ダサい)

#if false   // CommunityToolkit を使用しない実装
        model.PropertyChanged += Model_PropertyChanged;     // 解除を実装していません
#else
        IsActive = true;
#endif
    }

#if false   // CommunityToolkit を使用しない実装
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
#else
    // register myself
    protected override void OnActivated()
    {
        // 解除を実装していません
        Messenger.Register<DependencyInjection1ViewModel, PropertyChangedMessage<int>>(this, static (r, m) =>
        {
            // プロパティ名を参照すると密結合となってしまうので、理想的には通知専用型を用意するべきだと思います。
            switch (m.PropertyName)
            {
                case nameof(DependencyInjection1Model.Counter):
                    r.ModelValue = m.NewValue;
                    break;
            }
        });
    }
#endif
}

internal sealed partial class DependencyInjection1Model : ObservableRecipient
{
    [ObservableProperty]
#if false   // CommunityToolkit を使用しない実装
    // nothing
#else
    [NotifyPropertyChangedRecipients]
#endif
    int _counter;

    internal void CountUp() => Counter++;
}
