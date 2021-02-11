using CoreApp.Domain.Base;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CoreApp.Domain.Systems.Implementations
{
    public sealed class CoreModuleDm : BaseDomain<CoreModule>, ICoreModuleDm
    {
        #region Fields
        private readonly IRepository<CoreRoleModulePermission> _roleModulePermissionRep;
        private readonly IRepository<CorePermission> _corePermissionRep;
        private readonly IRepository<CoreUserRole> _coreUserRoleRep;
        private readonly IRepository<CoreRole> _coreRoleRep;
        #endregion

        #region Contructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rep"></param>
        /// <param name="roleModulePermissionRep"></param>
        /// <param name="corePermissionRep"></param>
        /// <param name="coreUserRoleRep"></param>
        /// <param name="coreRoleRep"></param>
        public CoreModuleDm(IRepository<CoreModule> rep,
            IRepository<CoreRoleModulePermission> roleModulePermissionRep,
            IRepository<CorePermission> corePermissionRep,
            IRepository<CoreUserRole> coreUserRoleRep,
            IRepository<CoreRole> coreRoleRep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep) : base(rep, httpContextAccessor, userRep)
        {
            _roleModulePermissionRep = roleModulePermissionRep;
            _corePermissionRep = corePermissionRep;
            _coreUserRoleRep = coreUserRoleRep;
            _coreRoleRep = coreRoleRep;
        }
        #endregion

        #region Utilities

        #endregion

        #region Methods
        public IList<CoreModule> GetModulesByRoleIds(Guid[] roleIds)
        {
            var roleModulePermissions = _roleModulePermissionRep.TableNoTracking.Where(p => roleIds.Contains(p.RoleId));
            var moduleIds = roleModulePermissions.Select(s => s.ModuleId).Distinct().ToArray();

            // Tolist Directly because this table not have much data
            var permissions = _corePermissionRep.GetAll();
            var modules = _rep.Table.Where(p => moduleIds.Contains(p.Id)).ToList();
            foreach (var module in modules)
            {
                var permissionIds = roleModulePermissions.Where(p => p.ModuleId == module.Id).Select(s => s.PermissionId).Distinct().ToArray();
                module.Permissions = permissions.Where(p => permissionIds.Contains(p.Id)).ToList();
            }

            return modules;
        }

        public IList<CoreModule> GetModulesByUser(Guid userId)
        {
            // Get all role assigned for user 
            var roleIdsByUser = _coreUserRoleRep.Table.Where(p => p.UserId == userId).Select(s => s.RoleId).ToArray();

            // If user have admin role so return all modules
            var checkUserHaveAdminRole = _coreRoleRep.Table.Any(a => roleIdsByUser.Contains(a.Id) && (a.IsAdmin || a.IsSuperAdmin));
            if (checkUserHaveAdminRole)
                return _rep.GetAll();

            return GetModulesByRoleIds(roleIdsByUser);
        }
        #endregion
    }
}
