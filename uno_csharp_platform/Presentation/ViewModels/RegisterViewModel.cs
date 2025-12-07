using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace uno_csharp_platform.Presentation.ViewModels;

public partial class RegisterViewModel : ObservableObject
{
    private readonly INavigator _navigator;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isLoading;

    public RegisterViewModel(INavigator navigator)
    {
        _navigator = navigator;
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Username) || 
            string.IsNullOrWhiteSpace(Email) || 
            string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Vui lòng điền đầy đủ thông tin";
            return;
        }

        if (Password != ConfirmPassword)
        {
            ErrorMessage = "Mật khẩu xác nhận không khớp";
            return;
        }

        if (Password.Length < 6)
        {
            ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự";
            return;
        }

        IsLoading = true;

        try
        {
            // Simulate API call
            await Task.Delay(1000);

            // Mock registration success
            await _navigator.NavigateViewModelAsync<LoginViewModel>(this);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Đăng ký thất bại: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task NavigateToLoginAsync()
    {
        await _navigator.NavigateViewModelAsync<LoginViewModel>(this);
    }
}