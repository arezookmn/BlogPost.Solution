using BlogPost.Blazor.UI.Services.Base;

namespace BlogPost.Blazor.UI.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterAsync(RegisterDTO registerDto);
        Task<bool> LoginAsync(LoginDTO loginDto);
        Task LogoutAsync();
    }
}
