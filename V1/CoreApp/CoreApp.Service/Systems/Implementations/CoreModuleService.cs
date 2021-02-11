using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Service.Base;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Authentication.Jwt;
using CoreApp.Common.Models;
using System.Collections;
using System.Collections.Generic;

namespace CoreApp.Service.Systems.Implementations
{
    public sealed class CoreModuleService : BaseService<CoreModule>, ICoreModuleService
    {
        #region Fields
        private readonly ICoreModuleDm _dm;
        #endregion

        #region Contructors
        public CoreModuleService(ICoreModuleDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion

        #region Utilities
        #endregion

        #region Methods
        public IList<CoreModule> GetModulesByCurrentUser()
        {
            return _dm.GetModulesByUser(CurrentUserId);
        }
        #endregion
    }
}
