using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HackTonTemplate.Models;

namespace HackTonTemplate.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public AuthenticationData GetAuthenticationData(User user)
        {
            var identityAccess = GetIdentity("Access", user);
            var identityRefresh = GetIdentity("Refresh", user);

            var now = DateTime.UtcNow;
            var expiresAccess = now.Add(TimeSpan.FromMinutes(AuthenticationTokenOptions.ACCESSLIFETIME));
            var expiresRefresh = now.Add(TimeSpan.FromMinutes(AuthenticationTokenOptions.REFRESHLIFETIME));

            return new AuthenticationData
            {
                ExpirationTime = expiresAccess,
                AccessToken = GetToken(identityAccess, expiresAccess),
                RefreshToken = GetToken(identityRefresh, expiresRefresh),
                IsSuccessful = true,
            };
        }
        private ClaimsIdentity GetIdentity(string type, User user)
        {
            var claims = new List<Claim>
            {
                new("TokenType", type),
                new("UserId", user.Id.ToString())
            };

            if (type == "Refresh") claims.Add(new Claim("UserKey", user.UserKey));

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private string GetToken(ClaimsIdentity identity, DateTime expires)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthenticationTokenOptions.ISSUER,
                audience: AuthenticationTokenOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(AuthenticationTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<AuthenticationData> ValidationOfRegistrationData(LoginOrRegistrationData loginOrRegistrationData)
        {
            if (await _userService.IsLoginBusy(loginOrRegistrationData.Login))
            {
                return new AuthenticationData(false, "Логин занят");
            }

            return new AuthenticationData(true);
        }
    }

    public interface IAuthenticationService
    {
        AuthenticationData GetAuthenticationData(User user);
        Task<AuthenticationData> ValidationOfRegistrationData(LoginOrRegistrationData loginOrRegistrationData);
    }
}
