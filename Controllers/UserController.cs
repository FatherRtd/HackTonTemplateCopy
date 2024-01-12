using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HackTonTemplate.Services;

namespace HackTonTemplate.Controllers
{
    public class 
        UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [Route("api/user/getCurrentUser")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId");
            long userId = long.Parse(userIdClaim!.Value);

            var user = await _userService.GetUser(userId);
            return Json(user);
        }

        [Authorize]
        [Route("api/user/event-register/{eventId}")]
        [HttpPost]
        public async Task EventRegistration(long eventId)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId");
            var userId = long.Parse(userIdClaim!.Value);

            await _userService.EventRegistration(eventId, userId);
        }

        [Authorize]
        [Route("api/user/event-unregister/{eventId}")]
        [HttpDelete]
        public async Task EventUnRegistration(long eventId)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId");
            var userId = long.Parse(userIdClaim!.Value);

            await _userService.EventUnRegistration(eventId, userId);
        }
    }
}
