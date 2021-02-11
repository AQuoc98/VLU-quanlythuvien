using CoreApp.Service.Systems.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using CoreApp.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class CoreConfigApiController : ModuleApiControllerBase<CoreConfig>
    {
        #region Fields
        private readonly ICoreConfigService _service;
        #endregion

        #region Contructors

        public CoreConfigApiController(ILogger<CoreConfigApiController> logger, ICoreConfigService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

        #region Methods
        [HttpGet]
        public IList<CoreConfigGroup> GetConfigGroup()
        {
            return _service.GetConfigGroup();
        }
        #endregion
    }
}
