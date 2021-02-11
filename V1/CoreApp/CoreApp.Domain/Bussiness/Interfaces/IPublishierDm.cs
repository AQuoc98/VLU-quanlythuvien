using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IPublishierDm : IBaseDomain<DoPublishier>
    {
        new DoPublishier GetById(Guid id);
        DoPublishier CheckingForName(DoPublishier entity);
        DoPublishier CheckingForUpdate(DoPublishier entity);
    }
}
