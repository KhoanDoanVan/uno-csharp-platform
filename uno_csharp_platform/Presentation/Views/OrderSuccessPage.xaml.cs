using uno_csharp_platform.Presentation.ViewModels;

namespace uno_csharp_platform.Presentation.Views;

public sealed partial class OrderSuccessPage : Page
{
    public OrderSuccessViewModel? ViewModel => DataContext as OrderSuccessViewModel;

    public OrderSuccessPage()
    {
        this.InitializeComponent();
    }
}