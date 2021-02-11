using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoMemberGroupApiController : ModuleApiControllerBase<DoMemberGroup>
    {
        #region Fields
        private readonly IMemberGroupService _service;
        #endregion

        #region Contructors

        public DoMemberGroupApiController(ILogger<DoMemberGroupApiController> logger, IMemberGroupService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

    }
}
