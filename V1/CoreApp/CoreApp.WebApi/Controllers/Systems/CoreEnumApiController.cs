using CoreApp.Service.Systems.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using CoreApp.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class CoreEnumApiController : ModuleApiControllerBase<CoreEnum>
    {
        #region Fields
        private readonly ICoreEnumService _service;
        #endregion

        #region Contructors

        public CoreEnumApiController(ILogger<CoreEnumApiController> logger, ICoreEnumService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

        #region Methods
        [HttpGet]
        public IList<CoreEnumValue> GetEnumValuesByEnum(string enumCode)
        {
            return _service.GetEnumValuesByEnum(enumCode);
        }

        [HttpGet]
        public IList<CoreEnumValue> GetEnumValuesByEnumId(Guid enumId)
        {
            return _service.GetEnumValuesByEnumId(enumId);
        }
        #endregion
    }
}
