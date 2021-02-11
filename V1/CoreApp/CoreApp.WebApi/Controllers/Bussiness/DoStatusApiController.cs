using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoStatusApiController : ModuleApiControllerBase<DoStatus>
    {
        #region Fields
        private readonly IStatusService _service;
        #endregion

        #region Contructors

        public DoStatusApiController(ILogger<DoStatusApiController> logger, IStatusService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

    }
}
