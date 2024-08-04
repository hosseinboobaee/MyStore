using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs.Account;
using Core.Services.Interfaces;
using Core.Utilities.Common;
using Core.Utilities.TokenService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MyStore.Controllers
{

    public class AccountController : SiteBaseController
    {
        #region costructor

        private IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, ITokenService token)
        {
            _userService = userService;
            _tokenService = token;
        }

        #endregion

        #region Register

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO register)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.RegisterUser(register);

                switch (res)
                {
                    case RegisterUserResult.EmailExists:
                        return JsonResponseStatus.Error(new { status = "EmailExist" });
                }
            }

            return JsonResponseStatus.Success();
        }

        #endregion

        #region Login

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.LoginUser(login);

                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponseStatus.NotFound(new { message = "کاربری با این نام یافت نشده است" });

                    case LoginUserResult.NotActivated:
                        return JsonResponseStatus.Error(new { message = "حساب کاربری شما فعال نشده است" });

                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByEmail(login.Email);
                        var token = _tokenService.CreateToken(user);

                        return JsonResponseStatus.Success(new { token = token, expireTime = 30, firstName = user.FirstName, lastName = user.LastName, userId = user.Id });
                }
            }

            return JsonResponseStatus.Error();
        }

        #endregion

        #region Sign Out

        [HttpGet("sing-out")]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.Error();
        }

        #endregion

        #region checkUserAuthenticate
        [HttpGet("check-auth")]
        [Authorize]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userService.GetUserById(Convert.ToInt32(userId));
                return JsonResponseStatus.Success(new
                {
                    userId = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                });
            }
            return JsonResponseStatus.Error();
        }
        #endregion
    }
}