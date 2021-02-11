using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoPolicyApiController : ModuleApiControllerBase<DoPolicy>
    {
        #region Fields
        private readonly IPolicyService _service;
        #endregion

        #region Contructors

        public DoPolicyApiController(ILogger<DoPolicyApiController> logger, IPolicyService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

    }
}
