namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

/// <summary>現在時刻の管理</summary>
internal sealed partial class ClockModel : ObservableRecipient
{
    [ObservableProperty]
    [NotifyPropertyChangedRecipients]   // ObservableRecipient の継承が必要です
    TimeOnly _updateTime;

    internal void Update()
    {
        // Model内部のプロパティ値を更新して、変更を通知します。疎結合
        UpdateTime = TimeOnly.FromDateTime(DateTime.Now);
    }
}
