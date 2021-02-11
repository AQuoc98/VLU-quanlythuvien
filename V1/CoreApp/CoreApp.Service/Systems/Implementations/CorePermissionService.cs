using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Service.Base;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Authentication.Jwt;
using System.Collections.Generic;
using System;

namespace CoreApp.Service.Systems.Implementations
{
    public sealed class CorePermissionService : BaseService<CoreRole>, ICorePermissionService
    {
        #region Fields
        private readonly ICorePermissionDm _dm;
        #endregion

        #region Contructors
        public CorePermissionService(ICorePermissionDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion

        #region Utilities
        #endregion

        #region Methods
        public IList<CorePermission> GetPermissions()
        {
            return _dm.GetPermissions();
        }

        public IList<CoreRole> GetRoleByCurrentUser()
        {
            return _dm.GetRoleByUser(CurrentUserId);
        }
        #endregion
    }
}
