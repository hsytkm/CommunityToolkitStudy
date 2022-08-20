namespace CommunityToolkitStudy.Wpf.Views.Mvvm.Messaging;

internal readonly record struct MyUnit(nint Value);

/// <summary>
/// イベント発生を通知します(ジェネリック値は使用されません)
/// </summary>
internal sealed class RequestCountUpUnitMessage : ValueChangedMessage<MyUnit>
{
    internal static RequestCountUpUnitMessage Shared { get; } = new(default);
    internal RequestCountUpUnitMessage(MyUnit value) : base(value) { }
}
