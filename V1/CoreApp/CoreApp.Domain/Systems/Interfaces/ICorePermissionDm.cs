using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Interfaces
{
    public interface ICorePermissionDm : IBaseDomain<CoreRole>
    {
        IList<CorePermission> GetPermissions();
        IList<CoreRole> GetRoleByUser(Guid userId);
    }
}
