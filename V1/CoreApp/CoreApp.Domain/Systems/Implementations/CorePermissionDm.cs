using CoreApp.Domain.Base;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CoreApp.Common.Constants;

namespace CoreApp.Domain.Systems.Implementations
{
    public sealed class CorePermissionDm : BaseDomain<CoreRole>, ICorePermissionDm
    {
        #region Fields
        private readonly IRepository<CorePermission> _corePermission;
        private readonly IRepository<CoreRoleModulePermission> _coreRoleModulePermission;
        private readonly IRepository<CoreUserRole> _coreUserRoleRep;
        private readonly IRepository<CoreRole> _coreRoleRep;
        #endregion

        #region Contructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="rep"></param>
        public CorePermissionDm(IRepository<CoreRole> rep,
            IRepository<CorePermission> corePermission,
            IRepository<CoreRoleModulePermission> coreRoleModulePermission,
            IRepository<CoreUserRole> coreUserRoleRep,
            IRepository<CoreRole> coreRoleRep,
            IHttpContextAccessor httpContextAccessor,
            IRepository<CoreUser> userRep) : base(rep, httpContextAccessor, userRep)
        {
            _corePermission = corePermission;
            _coreRoleModulePermission = coreRoleModulePermission;
            _coreUserRoleRep = coreUserRoleRep;
            _coreRoleRep = coreRoleRep;
        }
        #endregion

        #region Utilities

        #endregion

        #region Methods
        public override IList<CoreRole> GetAll()
        {
            return _rep.Table.Where(p => !p.IsAdmin).ToList();
        }

        public override CoreRole GetById(Guid id)
        {
            return _rep.Include(i => i.CoreRoleModulePermissions).Single(s => s.Id == id);
        }

        public IList<CorePermission> GetPermissions()
        {
            return _corePermission.GetAll();
        }

        public override int Update(CoreRole entity)
        {
            var permissionFromDb = _coreRoleModulePermission.Table.Where(p => p.RoleId == entity.Id).ToList();

            var permissionAddList = entity.CoreRoleModulePermissions.Where(p => !permissionFromDb.Any(a => a.ModuleId == p.ModuleId && a.PermissionId == p.PermissionId));
            var permissionUpdateList = entity.CoreRoleModulePermissions.Where(p => permissionFromDb.Any(a => a.ModuleId == p.ModuleId && a.PermissionId == p.PermissionId));
            var permissionDeleteList = permissionFromDb.Where(p => !entity.CoreRoleModulePermissions.Any(a => a.ModuleId == p.ModuleId && a.PermissionId == p.PermissionId));

            _coreRoleModulePermission.Insert(permissionAddList);
            _coreRoleModulePermission.Update(permissionUpdateList);
            _coreRoleModulePermission.Delete(permissionDeleteList);

            _rep.Update(entity);

            return SaveChanges();
        }

        public IList<CoreRole> GetRoleByUser(Guid userId)
        {
            // Get all role assigned for user 
            var roleIdsByUser = _coreUserRoleRep.Table.Where(p => p.UserId == userId).Select(s => s.RoleId).ToArray();

            // If user have admin role so return all modules
            var checkUserHaveAdminRole = _coreRoleRep.Table.Any(a => roleIdsByUser.Contains(a.Id) && a.IsAdmin);
            if (checkUserHaveAdminRole)
                return _rep.Table.Where(p => !p.IsSuperAdmin).ToList();

            var checkUserHaveSuperAdminRole = _coreRoleRep.Table.Any(a => roleIdsByUser.Contains(a.Id) && a.IsSuperAdmin);
            if (checkUserHaveSuperAdminRole)
                return _rep.GetAll();

            return _rep.Table.Where(p => !p.IsAdmin && !p.IsSuperAdmin).ToList();
        }
        #endregion
    }
}
