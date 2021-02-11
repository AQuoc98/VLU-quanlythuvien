using CoreApp.Service.Base;
using CoreApp.EntityFramework.Models;
using System.Collections.Generic;

namespace CoreApp.Service.Systems.Interfaces
{
    public interface ICorePermissionService : IBaseService<CoreRole>
    {
        IList<CorePermission> GetPermissions();
        IList<CoreRole> GetRoleByCurrentUser();
    }
}
