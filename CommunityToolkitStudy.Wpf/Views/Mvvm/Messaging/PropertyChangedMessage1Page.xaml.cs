using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

// [メッセンジャー - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
// [PropertyChangedMessage<T> Class (Microsoft.Toolkit.Mvvm.Messaging.Messages) | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.messages.propertychangedmessage-1)
public sealed partial class PropertyChangedMessage1Page : MyPageControlBase
{
    public PropertyChangedMessage1Page()
    {
        DataContext = new PropertyChangedMessage1ViewModel();
        InitializeComponent();
    }
}

internal sealed partial class PropertyChangedMessage1ViewModel
    : ObservableRecipient    // IsActive に必要です
{
    readonly ClockModel _clockModel = new();

    [ObservableProperty]
    string _latestTime = "";

    public PropertyChangedMessage1ViewModel() => IsActive = true;

    protected override void OnActivated()
    {
        // 3. 差出元は不明で(型とプロパティ名から判定で)メッセージを監視して値を取得します。疎結合
        Messenger.Register<PropertyChangedMessage1ViewModel, PropertyChangedMessage<TimeOnly>>(this, static (r, m) =>
        {
            // 型とプロパティ名で分岐できますが、差出人(この実装ではModel)に依存してしまいます。密結合ヨクナイ
            if (m.Sender.GetType() == typeof(ClockModel) && m.PropertyName is nameof(ClockModel.UpdateTime))
            {
                r.LatestTime = $"{m.Sender.GetType().Name}.{m.PropertyName} : {m.NewValue:HH:mm:ss.fff}";
            }

            // 以下がオススメ。差出人を知りません。
            //r.LatestTime = m.NewValue.ToString($"HH:mm:ss.fff");
        });
    }

    [RelayCommand]
    void RequestCurrentTime()
    {
        // 1. UI操作から Model.Method() を実行します。疎結合ではない
        _clockModel.Update();
    }

    [RelayCommand]
    void ClearTime() => LatestTime = "";
}
