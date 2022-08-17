using System.ComponentModel.DataAnnotations;
using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.ComponentModel;

// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/observableproperty#requesting-property-validationpublic
public sealed partial class ObservableValidator1Page : MyPageControlBase
{
    public ObservableValidator1Page()
    {
        DataContext = new ObservableValidator1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class ObservableValidator1ViewModel : ObservableValidator
{
    // ◆起動直後(空文字)の時にエラー判定できていないのが気になるけど調査していません。
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "3 or more is required")]
    [MinLength(3)]  // 2 or less is not allowed
    [MaxLength(6)]
    string _name = "";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Number is required")]
    [RegularExpression("[0-9]+$", ErrorMessage = "0 ~ 100 is required")]
    [Range(0, 100)]
    int _age;

    public string Message { get; } = "ReactiveProperty is recommended!  Error がない場合のみ値を通知できて便利です。";
}
