using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoPublishierApiController : ModuleApiControllerBase<DoPublishier>
    {
        #region Fields
        private readonly IPublishierService _service;
        #endregion

        #region Contructors

        public DoPublishierApiController(ILogger<DoPublishierApiController> logger, IPublishierService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

    }
}
