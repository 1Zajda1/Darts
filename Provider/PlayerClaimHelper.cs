using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Sdc.Provider
{
    public static class PlayerClaimHelper
    {
        public static int GetUserId(ClaimsIdentity user)
        {
            return int.Parse(user.Claims.FirstOrDefault(x => x.Type == "Id").Value);
        }
        public static ClaimsIdentity GetUser(AuthenticationState authState)
        {
            return authState.User.Identities.First();
        }
    }

}
