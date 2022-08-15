using CommunityToolkitStudy.Wpf.Views.Controls;

namespace CommunityToolkitStudy.Wpf.Services;

internal interface IPageSourceProvider
{
    string TypeName { get; }
    string Title { get; }
    string Subtitle { get; }
    string Description { get; }
    IReadOnlyCollection<string>? Keywords { get; }
    MyPageControlBase CreateControl();
    bool IsContain(string pattern);
}

internal sealed class PageSourceProvider<T> : IPageSourceProvider
    where T : MyPageControlBase
{
    public string TypeName { get; }
    public string Title { get; } = "";
    public string Subtitle { get; } = "";
    public string Description { get; } = "";
    public IReadOnlyCollection<string>? Keywords { get; }
    readonly List<string> _keys = new();

    public PageSourceProvider()
    {
        TypeName = typeof(T).ToString().Split('.')[^1];   // Typeから名前空間を除去する

        // 一度インスタンス化して、内部情報を吸い上げる
        if (Activator.CreateInstance<T>() is MyPageControlBase control)
        {
            Title = control.Title;
            Subtitle = control.Subtitle;
            Description = control.Description;
            Keywords = control.Keywords;

            _keys.Add(Title);
            _keys.Add(Subtitle);
            _keys.Add(Description);
            _keys.AddRange(Keywords);
        }
    }

    public MyPageControlBase CreateControl() => Activator.CreateInstance<T>();

    public bool IsContain(string pattern) =>
        _keys.Any(x => x.Contains(pattern, StringComparison.InvariantCultureIgnoreCase));
}
