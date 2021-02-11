using CoreApp.Service.Systems.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using CoreApp.EntityFramework.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class CoreTableApiController : ModuleApiControllerBase<CoreTable>
    {
        #region Fields
        private readonly ICoreTableService _service;
        #endregion

        #region Contructors

        public CoreTableApiController(ILogger<CoreTableApiController> logger, ICoreTableService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

        #region Methods
        [HttpGet]
        public IList<CoreDataType> GetDataType()
        {
            return _service.GetDataType();
        }
        #endregion
    }
}
