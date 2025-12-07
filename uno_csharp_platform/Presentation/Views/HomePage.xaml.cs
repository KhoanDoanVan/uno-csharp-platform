using uno_csharp_platform.Presentation.ViewModels;

namespace uno_csharp_platform.Presentation.Views;

public sealed partial class HomePage : Page
{
    public HomeViewModel? ViewModel => DataContext as HomeViewModel;

    public HomePage()
    {
        this.InitializeComponent();
        Loaded += HomePage_Loaded;
    }

    private async void HomePage_Loaded(object sender, RoutedEventArgs e)
    {
        if (ViewModel != null)
        {
            await ViewModel.LoadDataAsync();
        }
    }
}