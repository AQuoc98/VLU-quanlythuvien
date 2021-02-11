using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Interfaces
{
    public interface ICoreMenuDm : IBaseDomain<CoreMenu>
    {
        IList<CoreMenu> GetMenus(Guid userId);
        IList<CoreMenu> GetParentMenus();
    }
}
