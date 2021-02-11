using CoreApp.Service.Systems.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using CoreApp.EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreApp.Common.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.WebApi.Controllers.Systems
{
    public class CoreViewApiController : ModuleApiControllerBase<CoreView>
    {
        #region Fields
        private readonly ICoreViewService _service;
        #endregion

        #region Contructors

        public CoreViewApiController(ILogger<CoreViewApiController> logger, ICoreViewService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

        #region Methods
        [HttpPost]
        public QueryDataModel QueryData(QueryDataModel model)
        {
            return _service.QueryData(model);
        }

        [HttpPost]
        public SearchDataModel SearchData(SearchDataModel model)
        {
            return _service.SearchData(model);
        }

        [HttpGet]
        public IList<CoreViewColumn> GetViewDetails(Guid moduleId)
        {
            return _service.GetViewDetails(moduleId);
        }

        [HttpGet]
        public IList<CoreView> GetViewsByModule(Guid moduleId)
        {
            return _service.GetViewsByModule(moduleId);
        }

        [HttpGet]
        public GridViewFilterControl GetGridViewFilterSelectData(Guid moduleId)
        {
            return _service.GetGridViewFilterSelectData(moduleId);
        }
        #endregion
    }
}
