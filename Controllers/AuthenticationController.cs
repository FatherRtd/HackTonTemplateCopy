using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HackTonTemplate.Models;
using HackTonTemplate.Services;

namespace HackTonTemplate.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [Route("api/token/generate")]
        [HttpPost]
        public async Task<IActionResult> Generate([FromBody] LoginOrRegistrationData loginOrRegistrationData)
        {
            var user = await _userService.GetUserByLoginData(loginOrRegistrationData);
            if (user == null)
            {
                return Json(new AuthenticationData
                {
                    IsSuccessful = false,
                    Message = "Неправильный логин или пароль",
                });
            }

            return Json(_authenticationService.GetAuthenticationData(user));
        }

        [Authorize(Policy = "RefreshPolicy")]
        [Route("api/token/refresh")]
        [HttpGet]
        public async Task<IActionResult> Refresh()
        {
            var userKey = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserKey");
            if (userKey != null)
            {
                var user = await _userService.GetUserByKey(userKey.Value);
                if (user != null)
                {
                    return Json(_authenticationService.GetAuthenticationData(user));
                }
            }

            return Json(new AuthenticationData
            {
                IsSuccessful = false,
                Message = "Не удалось обновить токен доступа",
            });
        }

        [Route("api/token/registration")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] User newUser)
        {
            var user = await _userService.AddUser(newUser, UserRole.Participant);
            return Json(_authenticationService.GetAuthenticationData(user));
        }
    }
}
