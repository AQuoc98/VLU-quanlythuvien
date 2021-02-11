using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IFormatDm : IBaseDomain<DoFormat>
    {
        new DoFormat GetById(Guid id);
        DoFormat CheckingForName(DoFormat entity);
        DoFormat CheckingForUpdate(DoFormat entity);
    }
}
