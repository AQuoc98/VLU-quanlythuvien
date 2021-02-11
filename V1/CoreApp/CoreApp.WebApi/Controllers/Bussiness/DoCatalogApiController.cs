using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoCatalogApiController : ModuleApiControllerBase<DoCatalog>
    {
        #region Fields
        private readonly ICatalogService _service;
        #endregion

        #region Contructors

        public DoCatalogApiController(ILogger<DoCatalogApiController> logger, ICatalogService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

    }
}
