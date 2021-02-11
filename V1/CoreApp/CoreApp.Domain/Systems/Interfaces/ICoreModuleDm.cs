using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Interfaces
{
    public interface ICoreModuleDm : IBaseDomain<CoreModule>
    {
        IList<CoreModule> GetModulesByRoleIds(Guid[] roleIds);
        IList<CoreModule> GetModulesByUser(Guid userId);
    }
}
