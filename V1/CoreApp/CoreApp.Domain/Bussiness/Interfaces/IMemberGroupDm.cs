using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IMemberGroupDm : IBaseDomain<DoMemberGroup>
    {
        new DoMemberGroup GetById(Guid id);
        DoMemberGroup CheckingForName(DoMemberGroup entity);
        DoMemberGroup CheckingForUpdate(DoMemberGroup entity);
    }
}
