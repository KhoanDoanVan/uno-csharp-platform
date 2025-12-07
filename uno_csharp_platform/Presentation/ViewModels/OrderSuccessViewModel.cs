using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace uno_csharp_platform.Presentation.ViewModels;

public partial class OrderSuccessViewModel : ObservableObject
{
    private readonly INavigator _navigator;

    public OrderSuccessViewModel(INavigator navigator)
    {
        _navigator = navigator;
    }

    [RelayCommand]
    private async Task NavigateToHomeAsync()
    {
        await _navigator.NavigateViewModelAsync<HomeViewModel>(this);
    }
}