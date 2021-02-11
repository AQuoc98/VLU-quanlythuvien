using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface ICatalogDm : IBaseDomain<DoCatalog>
    {
        new DoCatalog GetById(Guid id);
        DoCatalog CheckingForName(DoCatalog entity);
        DoCatalog CheckingForUpdate(DoCatalog entity);
    }
}
