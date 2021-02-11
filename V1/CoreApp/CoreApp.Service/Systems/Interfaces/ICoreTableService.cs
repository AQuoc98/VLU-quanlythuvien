using CoreApp.Service.Base;
using CoreApp.EntityFramework.Models;
using System.Collections.Generic;

namespace CoreApp.Service.Systems.Interfaces
{
    public interface ICoreTableService : IBaseService<CoreTable>
    {
        IList<CoreDataType> GetDataType();
    }
}
