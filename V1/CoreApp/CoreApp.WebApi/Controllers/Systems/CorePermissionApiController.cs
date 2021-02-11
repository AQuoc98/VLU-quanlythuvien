using CoreApp.Service.Systems.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using CoreApp.EntityFramework.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class CorePermissionApiController : ModuleApiControllerBase<CoreRole>
    {
        #region Fields
        private readonly ICorePermissionService _service;
        #endregion

        #region Contructors

        public CorePermissionApiController(ILogger<CorePermissionApiController> logger, ICorePermissionService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

        #region Methods
        [HttpGet]
        public IList<CorePermission> GetPermissions()
        {
            return _service.GetPermissions();
        }
        [HttpGet]
        public IList<CoreRole> GetRoleByCurrentUser()
        {
            return _service.GetRoleByCurrentUser();
        }
        #endregion
    }
}
