using CoreApp.Service.Base;
using CoreApp.EntityFramework.Models;
using System.Collections.Generic;

namespace CoreApp.Service.Systems.Interfaces
{
    public interface ICoreMenuService : IBaseService<CoreMenu>
    {
        IList<CoreMenu> GetMenus();
        IList<CoreMenu> GetParentMenus();
    }
}
