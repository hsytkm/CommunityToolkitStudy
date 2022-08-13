using System.Windows.Controls;

namespace CommunityToolkitStudy.Wpf.Views;

public partial class PagesListBoxPage : UserControl
{
    public PagesListBoxPage()
    {
        DataContext = App.GetViewModel<PagesListBoxPage>();
        InitializeComponent();

        Loaded += PagesListBoxPage_Loaded;
    }

    private void PagesListBoxPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        filterTextBox.Focus();
        Loaded -= PagesListBoxPage_Loaded;
    }

}

