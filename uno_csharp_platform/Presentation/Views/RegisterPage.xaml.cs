using uno_csharp_platform.Presentation.ViewModels;

namespace uno_csharp_platform.Presentation.Views;

public sealed partial class RegisterPage : Page
{
    public RegisterViewModel? ViewModel => DataContext as RegisterViewModel;

    public RegisterPage()
    {
        this.InitializeComponent();
    }
}