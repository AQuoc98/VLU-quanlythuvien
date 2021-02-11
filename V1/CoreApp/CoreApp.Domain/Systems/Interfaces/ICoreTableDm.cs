using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Interfaces
{
    public interface ICoreTableDm : IBaseDomain<CoreTable>
    {
        IList<CoreDataType> GetDataType();
        bool CheckAliasExisted(CoreTable entity);

    }
}
