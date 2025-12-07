using uno_csharp_platform.Presentation.ViewModels;

namespace uno_csharp_platform.Presentation.Views;

public sealed partial class CheckoutPage : Page
{
    public CheckoutViewModel? ViewModel => DataContext as CheckoutViewModel;

    public CheckoutPage()
    {
        this.InitializeComponent();
        Loaded += CheckoutPage_Loaded;
    }

    private async void CheckoutPage_Loaded(object sender, RoutedEventArgs e)
    {
        if (ViewModel != null)
        {
            await ViewModel.LoadDataAsync();
        }
    }
}