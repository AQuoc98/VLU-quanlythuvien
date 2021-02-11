using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IDoBookDm : IBaseDomain<DoBook>
    {
        new DoBook GetById(Guid id);
        DoBook CheckingForISBN(DoBook entity);
        DoBook CheckingForUpdate(DoBook entity);
    }
}
