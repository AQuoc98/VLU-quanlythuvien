using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoAuthorApiController : ModuleApiControllerBase<DoAuthor>
    {
        #region Fields
        private readonly IAuthorService _service;
        #endregion

        #region Contructors

        public DoAuthorApiController(ILogger<DoAuthorApiController> logger, IAuthorService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion
        
    }
}
