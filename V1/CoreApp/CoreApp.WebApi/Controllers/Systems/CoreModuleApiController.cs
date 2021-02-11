using CoreApp.Service.Systems.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using CoreApp.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreApp.Common.Models;
using System.Collections.Generic;
using System;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class CoreModuleApiController : ModuleApiControllerBase<CoreModule>
    {
        #region Fields
        private readonly ICoreModuleService _service;
        #endregion

        #region Contructors

        public CoreModuleApiController(ILogger<CoreModuleApiController> logger, ICoreModuleService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

        #region Methods
        [HttpGet]
        public IList<CoreModule> GetModulesByCurrentUser()
        {
            return _service.GetModulesByCurrentUser();
        }
        #endregion
    }
}
