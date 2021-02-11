using CoreApp.Authentication.Helpers;
using CoreApp.Authentication.Jwt;
using CoreApp.Authentication.Models;
using CoreApp.Common.Constants;
using CoreApp.Common.Enums;
using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Systems.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CoreApp.WebApi.Controllers.Authen
{
    [ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    public class AuthenticationApiController : ControllerBase
    {
        #region Fields
        protected readonly ILogger<AuthenticationApiController> _logger;
        private readonly IUserManager _userManager;
        private readonly ICoreUserService _userService;
        #endregion

        #region Contructors

        public AuthenticationApiController(ILogger<AuthenticationApiController> logger, IUserManager userManager, ICoreUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] SignInModel model)
        {
            model.LoginTypeCode = SignInConstants.LoginType.Email;
            var jwtResult = await _userManager.SignIn(model);
            if (jwtResult.Status == ResultStatus.Success)
            {
                _logger.LogInformation("User logged in.");
            }
            else
            {
                _logger.LogInformation("Invalid login attempt.");
            }
            return new JsonResult(jwtResult.ExtendData);
        }
        #endregion
    }
}
