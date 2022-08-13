using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CommunityToolkitStudy.Wpf.Views.Controls;

public abstract class MyPageControlBase : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(MyPageControlBase), new PropertyMetadata(""));
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty SubtitleProperty =
        DependencyProperty.Register(nameof(Subtitle), typeof(string), typeof(MyPageControlBase), new PropertyMetadata(""));
    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(MyPageControlBase), new PropertyMetadata(""));
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty KeywordsProperty =
        DependencyProperty.Register(nameof(Keywords), typeof(IReadOnlyCollection<string>), typeof(MyPageControlBase),
            new PropertyMetadata(Array.Empty<string>()));
    public IReadOnlyCollection<string> Keywords
    {
        get => (IReadOnlyCollection<string>)GetValue(KeywordsProperty);
        set => SetValue(KeywordsProperty, value);
    }

    public MyPageControlBase()
    {
        Title = this.GetType().ToString().Split('.')[^1].Replace("Page", "");
    }
}
