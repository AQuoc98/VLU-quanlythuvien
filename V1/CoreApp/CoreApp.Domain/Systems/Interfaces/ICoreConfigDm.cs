using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Interfaces
{
    public interface ICoreConfigDm : IBaseDomain<CoreConfig>
    {
        IList<CoreConfigGroup> GetConfigGroup();
    }
}
