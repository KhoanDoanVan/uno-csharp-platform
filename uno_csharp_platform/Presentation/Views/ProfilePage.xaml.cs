using uno_csharp_platform.Presentation.ViewModels;

namespace uno_csharp_platform.Presentation.Views;

public sealed partial class ProfilePage : Page
{
    public ProfileViewModel? ViewModel => DataContext as ProfileViewModel;

    public ProfilePage()
    {
        this.InitializeComponent();
    }
}