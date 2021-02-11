using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoFormatApiController : ModuleApiControllerBase<DoFormat>
    {
        #region Fields
        private readonly IFormatService _service;
        #endregion

        #region Contructors

        public DoFormatApiController(ILogger<DoFormatApiController> logger, IFormatService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

    }
}
