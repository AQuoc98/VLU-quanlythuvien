using CoreApp.Service.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System.Collections.Generic;

namespace CoreApp.Service.Systems.Interfaces
{
    public interface ICoreConfigService : IBaseService<CoreConfig>
    {
        ConfigData GetAppConfig();
        IList<CoreConfigGroup> GetConfigGroup();
    }
}
