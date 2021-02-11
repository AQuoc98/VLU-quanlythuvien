using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class CoreUserApiController : ModuleApiControllerBase<CoreUser>
    {
        #region Fields
        private readonly ICoreUserService _service;
        #endregion

        #region Contructors

        public CoreUserApiController(ILogger<CoreUserApiController> logger, ICoreUserService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

        #region Methods
        [HttpPost]
        public virtual ResultModel EditProfile(CoreUser entity)
        {
            return _service.EditProfile(entity);
        }
        #endregion
    }
}
