using CoreApp.Common.Helpers;
using CoreApp.Domain.Base;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CoreApp.EntityFramework.ViewModels;
using Microsoft.EntityFrameworkCore;
using CoreApp.Common.Constants;

namespace CoreApp.Domain.Systems.Implementations
{
    public sealed class CoreUserDm : BaseDomain<CoreUser>, ICoreUserDm
    {
        #region Fields
        private readonly IRepository<CoreCredentialType> _credentialTypeRep;
        private readonly IRepository<CoreCredential> _credentialRep;
        private readonly IRepository<CoreUserRole> _userRoleRep;
        private readonly IRepository<CoreRole> _roleRep;

        #endregion

        #region Contructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="rep"></param>
        /// <param name="credentialTypeRep">Credential Type Repository</param>
        /// <param name="credentialRep">Credential Repository</param>
        /// <param name="userRoleRep">User Role Repository</param>
        /// <param name="roleRep">Role Repository</param>
        /// <param name="permissionRep">Permission Repository</param>
        /// <param name="rolePermissionRep">Role Permission Repository</param>
        public CoreUserDm(IRepository<CoreUser> rep,
            IRepository<CoreCredentialType> credentialTypeRep,
            IRepository<CoreCredential> credentialRep,
            IRepository<CoreUserRole> userRoleRep,
            IRepository<CoreRole> roleRep, IHttpContextAccessor httpContextAccessor) : base(rep, httpContextAccessor, rep)
        {
            _credentialTypeRep = credentialTypeRep;
            _credentialRep = credentialRep;
            _userRoleRep = userRoleRep;
            _roleRep = roleRep;
        }
        #endregion

        #region Utilities

        #endregion

        #region Methods
        public override CoreUser GetById(Guid id)
        {
            var result = _rep.Include(c => c.CoreCredentials, r => r.CoreUserRoles)
                .Include(c => c.CoreUserRoles)
                .ThenInclude(t => t.Role)
                .AsNoTracking().Single(s => s.Id == id);
            var roleIds = result.CoreUserRoles.Select(s => s.RoleId).ToArray();
            result.IsSuperAdmin = _roleRep.Table.Any(a => roleIds.Contains(a.Id) && a.IsAdmin);

            return result;
        }

        public override int Update(CoreUser entity)
        {
            var oldFirstCredential = _credentialRep.TableNoTracking.FirstOrDefault(p => p.UserId == entity.Id);


            var newFirstCredential = entity.CoreCredentials.First();
            if (newFirstCredential.Secret.Length == 0)
            {
                newFirstCredential.Secret = oldFirstCredential.Secret;
            }

            // Roles
            var userRoleFromDb = _userRoleRep.Table.Where(p => p.UserId == entity.Id).ToList();

            var userRoleAddList = entity.CoreUserRoles.Where(p => !userRoleFromDb.Any(a => a.RoleId == p.RoleId));
            var userRoleUpdateList = entity.CoreUserRoles.Where(p => userRoleFromDb.Any(a => a.RoleId == p.RoleId));
            var userRoleDeleteList = userRoleFromDb.Where(p => !entity.CoreUserRoles.Any(a => a.RoleId == p.RoleId));

            _userRoleRep.Insert(userRoleAddList);
            _userRoleRep.Update(userRoleUpdateList);
            _userRoleRep.Delete(userRoleDeleteList);

            entity.CoreCredentials.Clear();

            _rep.Update(entity);
            _credentialRep.Update(newFirstCredential);

            return SaveChanges();
        }

        public CoreCredentialType GetCredentialTypeByCode(string code)
        {
            return _credentialTypeRep.Table.SingleOrDefault(s => s.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        public CoreCredential GetCredential(string identifier, string secret, Guid credentialTypeId)
        {
            return _credentialRep.Table.FirstOrDefault(c => c.CredentialTypeId == credentialTypeId &&
            string.Equals(c.Identifier, identifier, StringComparison.OrdinalIgnoreCase) &&
            EncryptHelper.ValidateHash(secret, c.Secret)
            || EncryptHelper.ValidateHash(secret, c.NewSecret));
            //|| EncryptHelper.ValidateHash(secret,c.cNewSecret));
        }

        public CoreCredential GetCredentialByIdentifier(string identifier)
        {
            return _credentialRep.Table.FirstOrDefault(c => string.Equals(c.Identifier, identifier, StringComparison.OrdinalIgnoreCase));
        }

        public IList<CoreRole> GeRolesByUserId(Guid userId)
        {
            var roleIds = _userRoleRep.Table.Where(p => p.UserId == userId).Select(s => s.RoleId).ToArray();
            return _roleRep.Table.Where(p => roleIds.Contains(p.Id)).ToList();
        }

        public CoreCredential CheckAccountExisted(CoreCredential entity)
        {
            return _credentialRep.Table.FirstOrDefault(c => c.Id != entity.Id && !string.IsNullOrEmpty(entity.Identifier) && string.Equals(c.Identifier, entity.Identifier, StringComparison.OrdinalIgnoreCase));
        }
        public CoreUser CheckingForUpdate(CoreUser entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && !string.IsNullOrEmpty(entity.Phone) && string
            .Equals(c.Phone, entity.Phone, StringComparison.OrdinalIgnoreCase));
        }
        public CoreUser CheckingForEmail(CoreUser entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && !string.IsNullOrEmpty(entity.Email) && string
            .Equals(c.Email, entity.Email, StringComparison.OrdinalIgnoreCase));
        }
        public CoreUser CheckingForName(CoreUser entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && !string.IsNullOrEmpty(entity.Name) && string
            .Equals(c.Name, entity.Name, StringComparison.OrdinalIgnoreCase));
        }
        public CoreUser CheckPhoneExisted(CoreUser entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Phone == entity.Phone && !string.IsNullOrEmpty(entity.Phone) && string
            .Equals(c.Phone, entity.Phone, StringComparison.OrdinalIgnoreCase));
        }
        public CoreUser GetUserByIdentifier(string identifier)
        {
            var result = _rep.Table.FirstOrDefault(c => (!string.IsNullOrEmpty(identifier) && string.Equals(c.Email, identifier, StringComparison.OrdinalIgnoreCase)) || (!string.IsNullOrEmpty(identifier) && string.Equals(c.Phone, identifier, StringComparison.OrdinalIgnoreCase)));
            if (result == null)
            {
                var CoreCredential = _credentialRep.Table.FirstOrDefault(x => string.Equals(x.Identifier, identifier, StringComparison.OrdinalIgnoreCase));
                if (CoreCredential == null) return result;
                result = _rep.Table.FirstOrDefault(c => c.CoreCredentials.Contains(CoreCredential));
            }
            return result;
        }

        public int ChangePassword(ChangePasswordVM entity)
        {
            var credentials = _credentialRep.Table.Where(x => x.UserId == CurrentUserId).ToList();
            var info = credentials.FirstOrDefault();
            info.Secret = entity.NewSecret.Length > 0 ? EncryptHelper.Hash(entity.NewSecret) : info.Secret;
            if (info == null)
            {
                return -1;
            }
            if (!EncryptHelper.ValidateHash(entity.Secret, info.Secret))
            {
                return -2;
            }

            _credentialRep.Update(info);
            return _credentialRep.SaveChanges();
        }

        public CoreUser GetCurrentUser()
        {
            return CurrentUser;
        }

        public override int Delete(IEnumerable<CoreUser> entities)
        {
            var userIdsDelete = entities.Select(x => x.Id).ToArray();
            return base.Delete(entities);
        }
        public int UpdateUserMobile(CoreUser entity)
        {
            _rep.Update(entity);
            return _rep.SaveChanges();
        }
        #endregion
    }
}
