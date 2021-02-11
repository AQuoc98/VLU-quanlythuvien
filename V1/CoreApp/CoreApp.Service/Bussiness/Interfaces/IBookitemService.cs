using CoreApp.EntityFramework.Models;
using CoreApp.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Service.Bussiness.Interfaces
{
    public interface IBookitemService : IBaseService<DoBookItem>
    {
        IList<DoBookItem> GetByBarCode(IEnumerable<string> barCodes);
    }
}
