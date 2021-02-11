using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IStatusDm : IBaseDomain<DoStatus>
    {
        new DoStatus GetById(Guid id);
        DoStatus CheckingForStatus(DoStatus entity);
        DoStatus CheckingForUpdate(DoStatus entity);


    }
}
