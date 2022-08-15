using System.Windows.Controls;

namespace CommunityToolkitStudy.Wpf.Views;

public partial class PagesListBoxPage : UserControl
{
    public PagesListBoxPage()
    {
        // ViewType から ViewModelType を引っ張っています
        DataContext = App.Current.GetViewModel<PagesListBoxPage>();
        InitializeComponent();

        Loaded += PagesListBoxPage_Loaded;
    }

    private void PagesListBoxPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        filterTextBox.Focus();
        Loaded -= PagesListBoxPage_Loaded;
    }

}
