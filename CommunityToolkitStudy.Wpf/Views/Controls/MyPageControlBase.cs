using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

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
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(MyPageControlBase),
            new FrameworkPropertyMetadata("", (sender, e) =>
            {
                var self = (MyPageControlBase)sender;
                self.HeaderPanel = self.GetHeaderPanel((string)e.NewValue);
            }));
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty KeywordsProperty =
        DependencyProperty.Register(nameof(Keywords), typeof(IReadOnlyList<string>), typeof(MyPageControlBase),
            new PropertyMetadata(Array.Empty<string>()));
    public IReadOnlyList<string> Keywords
    {
        get => (IReadOnlyList<string>)GetValue(KeywordsProperty);
        set => SetValue(KeywordsProperty, value);
    }

    public static readonly DependencyProperty HeaderPanelProperty =
        DependencyProperty.Register(nameof(HeaderPanel), typeof(Panel), typeof(MyPageControlBase));
    public Panel HeaderPanel
    {
        get => (Panel)GetValue(HeaderPanelProperty);
        set => SetValue(HeaderPanelProperty, value);
    }

    readonly string _baseLinkUrl;

    public MyPageControlBase()
    {
        var splits = this.GetType().ToString().Split('.');
        Title = splits[^1].Replace("Page", "");

        var baseUrl = @"https://github.com/hsytkm/CommunityToolkitStudy/blob/master/CommunityToolkitStudy.Wpf/";
        var absUrl = string.Join('/', splits[2..]);
        _baseLinkUrl = baseUrl + absUrl;
    }

    private Panel GetHeaderPanel(string description)
    {
        //<Grid Margin="0,0,0,10" DockPanel.Dock="Top">
        //    <Grid.ColumnDefinitions>
        //        <ColumnDefinition Width="*" />
        //        <ColumnDefinition Width="Auto" />
        //        <ColumnDefinition Width="Auto" />
        //    </Grid.ColumnDefinitions>
        //    <TextBlock
        //        FontSize="{StaticResource FontSizeLarge}"
        //        Text="{Binding Description, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type controls:MyPageControlBase}}}"
        //        TextDecorations="Underline" />
        //    <TextBlock
        //        Grid.Column="1"
        //        Margin="20,0"
        //        VerticalAlignment="Center">
        //        <Hyperlink NavigateUri="{Binding XamlLink, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type controls:MyPageControlBase}}}" RequestNavigate="RequestNavigate">
        //            Link(.xaml)
        //        </Hyperlink>
        //    </TextBlock>
        //    <TextBlock Grid.Column="2" VerticalAlignment="Center">
        //        <Hyperlink NavigateUri="{Binding XamlLink, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type controls:MyPageControlBase}}}" RequestNavigate="RequestNavigate">
        //            Link(.xaml.cs)
        //        </Hyperlink>
        //    </TextBlock>
        //</Grid>
        static void RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var processStartInfo = new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true };
            _ = Process.Start(processStartInfo);
            e.Handled = true;
        }

        var grid = new Grid()
        {
            Margin = new Thickness(0, 0, 0, 10),
        };
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

        // Column=0
        var titleTextBlock = new TextBlock
        {
            Text = description,
            FontSize = 18,  // FontSizeLarge
            TextDecorations = TextDecorations.Underline,
            HorizontalAlignment = HorizontalAlignment.Left,
        };
        Grid.SetColumn(titleTextBlock, 0);
        grid.Children.Add(titleTextBlock);

        // Column=1
        var xamlLinkTextBlock = new TextBlock()
        {
            FontSize = 14,  // FontSizeMiddle
            Margin = new Thickness(10, 0, 10, 0),
            VerticalAlignment = VerticalAlignment.Center,
        };
        var xamlHyperLink = new Hyperlink(new Run("Link(.xaml)"))
        {
            NavigateUri = new Uri(_baseLinkUrl + ".xaml"),
        };
        xamlHyperLink.RequestNavigate += RequestNavigate;
        xamlLinkTextBlock.Inlines.Add(xamlHyperLink);
        Grid.SetColumn(xamlLinkTextBlock, 1);
        grid.Children.Add(xamlLinkTextBlock);

        // Column=2
        var xamlCsLinkTextBlock = new TextBlock()
        {
            FontSize = 14,  // FontSizeMiddle
            Margin = new Thickness(10, 0, 10, 0),
            VerticalAlignment = VerticalAlignment.Center,
        };
        var xamlCsHyperLink = new Hyperlink(new Run("Link(.xaml.cs)"))
        {
            NavigateUri = new Uri(_baseLinkUrl + ".xaml.cs"),
        };
        xamlCsHyperLink.RequestNavigate += RequestNavigate;
        xamlCsLinkTextBlock.Inlines.Add(xamlCsHyperLink);
        Grid.SetColumn(xamlCsLinkTextBlock, 2);
        grid.Children.Add(xamlCsLinkTextBlock);

        return grid;
    }

}
