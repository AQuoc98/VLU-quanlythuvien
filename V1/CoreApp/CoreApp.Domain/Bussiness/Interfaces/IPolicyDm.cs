using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IPolicyDm : IBaseDomain<DoPolicy>
    {
        new DoPolicy GetById(Guid id);
        DoPolicy checkExistMemberGroup(DoPolicy entity);
        DoPolicy CheckingForUpdate(DoPolicy entity);


    }
}

