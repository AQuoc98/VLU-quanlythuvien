using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Service.Base;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Authentication.Jwt;
using System.Collections.Generic;

namespace CoreApp.Service.Systems.Implementations
{
    public sealed class CoreMenuService : BaseService<CoreMenu>, ICoreMenuService
    {
        #region Fields
        private readonly ICoreMenuDm _dm;
        #endregion

        #region Contructors
        public CoreMenuService(ICoreMenuDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion

        #region Utilities
        #endregion

        #region Methods
        public IList<CoreMenu> GetMenus()
        {
            return _dm.GetMenus(CurrentUserId);
        }
        public IList<CoreMenu> GetParentMenus()
        {
            return _dm.GetParentMenus();
        }
        #endregion
    }
}
