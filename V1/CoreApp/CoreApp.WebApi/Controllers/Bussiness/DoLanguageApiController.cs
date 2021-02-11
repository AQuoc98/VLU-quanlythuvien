using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.Service.Bussiness.Interfaces;
using CoreApp.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreApp.WebApi.Controllers.Bussiness
{
    public class DoLanguageApiController : ModuleApiControllerBase<DoLanguage>
    {
        #region Fields
        private readonly ILanguageService _service;
        #endregion

        #region Contructors

        public DoLanguageApiController(ILogger<DoLanguageApiController> logger, ILanguageService service)
            : base(logger, service)
        {
            _service = service;
        }

        #endregion

    }
}
