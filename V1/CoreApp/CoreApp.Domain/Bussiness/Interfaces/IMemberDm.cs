using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IMemberDm : IBaseDomain<DoMember>
    {
        new DoMember GetById(Guid id);
        DoMember CheckingForExistMemberCode(DoMember entity);
        DoMember CheckingUpdateExistMemberCode(DoMember entity);


    }
}
