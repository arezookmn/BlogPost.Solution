using Blazored.LocalStorage;
using BlogPost.Blazor.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogPost.Blazor.UI.Services.AuthenticationService
{
    public class AuthenticationService : ApiAuthenticationStateProvider, IAuthenticationService
    {
        private readonly IClient _client;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthenticationService(IClient client, ILogger<AuthenticationService> logger, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider) : base(localStorageService)
        {
            _client = client;
            _logger = logger;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDto)
        {
            var user = await _client.UserAsync(registerDto);
            if (user == null) return false;
            else
            return await LoginAsync(
                new LoginDTO(){Email = registerDto.Email, Password = registerDto.Password});
        }


        public async Task<bool> LoginAsync(LoginDTO loginDto)
        {
            AuthenticationResponse? response = await _client.LoginAsync(loginDto);

            return await AuthenticateUser(response.Token);
        }


        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync("accessToken");
            ClaimsPrincipal nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }

        private async Task<bool> AuthenticateUser(string? responseToken)
        {
            await _localStorageService.SetItemAsync("accessToken", responseToken);

            if (responseToken != null)
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = tokenHandler.ReadJwtToken(responseToken);
                var claims = token.Claims.ToList();
                claims.Add(new Claim(ClaimTypes.Name, token.Subject));

                AuthenticationState? authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));

                var authStateTask = Task.FromResult(authState);
                NotifyAuthenticationStateChanged(authStateTask);

                return true;
            }

            return false;
        }

    }
}
