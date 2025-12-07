using uno_csharp_platform.Presentation.ViewModels;

namespace uno_csharp_platform.Presentation.Views;

public sealed partial class LoginPage : Page
{
    public LoginViewModel? ViewModel => DataContext as LoginViewModel;

    public LoginPage()
    {
        this.InitializeComponent();
    }
}