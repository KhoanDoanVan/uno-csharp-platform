using uno_csharp_platform.Presentation.ViewModels;

namespace uno_csharp_platform.Presentation.Views;

public sealed partial class ProductDetailPage : Page
{
    public ProductDetailViewModel? ViewModel => DataContext as ProductDetailViewModel;

    public ProductDetailPage()
    {
        this.InitializeComponent();
    }
}