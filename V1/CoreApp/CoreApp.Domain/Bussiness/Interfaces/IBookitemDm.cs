using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IBookitemDm : IBaseDomain<DoBookItem>
    {
        new DoBookItem GetById(Guid id);
        DoBookItem CheckingForBarcode(DoBookItem entity);
        DoBookItem CheckingForUpdate(DoBookItem entity);
        IList<DoBookItem> GetByBarCode(IEnumerable<string> barCodes);
    }
}
