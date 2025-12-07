using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace uno_csharp_platform.Presentation.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private readonly INavigator _navigator;

    public ProfileViewModel(INavigator navigator)
    {
        _navigator = navigator;
    }

    [RelayCommand]
    private async Task NavigateToHomeAsync()
    {
        await _navigator.NavigateViewModelAsync<HomeViewModel>(this);
    }

    [RelayCommand]
    private async Task NavigateToCartAsync()
    {
        await _navigator.NavigateViewModelAsync<CartViewModel>(this);
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        // Clear any user session data here
        await _navigator.NavigateViewModelAsync<LoginViewModel>(this);
    }
}