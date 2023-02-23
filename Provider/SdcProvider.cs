using Microsoft.AspNetCore.Components.Authorization;
using Sdc.Model;
using System.Security.Claims;

namespace Sdc.Provider
{
    public class SdcProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(new AuthenticationState(currentUser));

        public Task MarkUserAsAuthenticated(Player player)
        {
            var loginTask = LogInAsyncCore(player);
            NotifyAuthenticationStateChanged(loginTask);

            return loginTask;

            async Task<AuthenticationState> LogInAsyncCore(Player player)
            {
                var user = await LoginWithExternalProviderAsync(player);
                currentUser = user;

                return new AuthenticationState(currentUser);
            }
        }

        private Task<ClaimsPrincipal> LoginWithExternalProviderAsync(Player player)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, player.UserName),
                    new Claim("Id", player.Id.ToString()),
                    new Claim(ClaimTypes.IsPersistent, "true"),
                    new Claim(ClaimTypes.Role, player.IsAdmin ? LoginConstants.ROLE_ADMIN : LoginConstants.ROLE_USER)
                }, "apiauth_type"));
            return Task.FromResult(authenticatedUser);
        }



        public void MarkUserAsLoggedOut()
        {
            currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));
        }
    }
}
