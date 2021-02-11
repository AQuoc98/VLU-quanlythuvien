using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoRackApiController : ModuleApiControllerBase<DoRack>
    {
        #region Fields
        private readonly IRackService _service;
        #endregion

        #region Contructors

        public DoRackApiController(ILogger<DoRackApiController> logger, IRackService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

    }
}
