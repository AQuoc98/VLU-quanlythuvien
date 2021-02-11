using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IRackDm : IBaseDomain<DoRack>
    {
        new DoRack GetById(Guid id);
        DoRack CheckingForLocation(DoRack entity);
        DoRack CheckingForNumber(DoRack entity);
        DoRack CheckingForUpdate(DoRack entity);
    }
}
