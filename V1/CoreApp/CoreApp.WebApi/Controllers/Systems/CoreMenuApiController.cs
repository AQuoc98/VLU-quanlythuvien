using CoreApp.Service.Systems.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using CoreApp.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreApp.Common.Models;
using System.Collections.Generic;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class CoreMenuApiController : ModuleApiControllerBase<CoreMenu>
    {
        #region Fields
        private readonly ICoreMenuService _service;
        #endregion

        #region Contructors

        public CoreMenuApiController(ILogger<CoreMenuApiController> logger, ICoreMenuService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

        #region Methods
        [HttpGet]
        public IList<CoreMenu> GetMenus()
        {
            return _service.GetMenus();
        }
        [HttpGet]
        public IList<CoreMenu> GetParentMenus()
        {
            return _service.GetParentMenus();
        }
        #endregion
    }
}
