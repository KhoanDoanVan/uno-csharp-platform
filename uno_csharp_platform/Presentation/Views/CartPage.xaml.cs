using uno_csharp_platform.Presentation.ViewModels;

namespace uno_csharp_platform.Presentation.Views;

public sealed partial class CartPage : Page
{
    public CartViewModel? ViewModel => DataContext as CartViewModel;

    public CartPage()
    {
        this.InitializeComponent();
        Loaded += CartPage_Loaded;
    }

    private async void CartPage_Loaded(object sender, RoutedEventArgs e)
    {
        if (ViewModel != null)
        {
            await ViewModel.LoadCartAsync();
        }
    }
}