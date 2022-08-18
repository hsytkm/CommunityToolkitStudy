namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

internal readonly record struct MyUnit(nint Value);

/// <summary>
/// 情報の更新を要求します(ジェネリック値は使用していません)
/// </summary>
internal sealed class RequestLatestInfoUnitMessage : ValueChangedMessage<MyUnit>
{
    internal static RequestLatestInfoUnitMessage Shared { get; } = new(default);
    internal RequestLatestInfoUnitMessage(MyUnit value) : base(value) { }
}
