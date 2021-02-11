using CoreApp.Domain.Bussiness.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CoreApp.EntityFramework.ViewModels;
using Microsoft.EntityFrameworkCore;
using CoreApp.Common.Constants;
using CoreApp.Common.Helpers;
using CoreApp.Domain.Base;

namespace CoreApp.Domain.Bussiness.Implementations
{
    public sealed class DoPolicyDm : BaseDomain<DoPolicy>, IPolicyDm
    {

        public DoPolicyDm(IRepository<DoPolicy> rep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep)
            : base(rep, httpContextAccessor, userRep)
        {
        }
        #region
        public override DoPolicy GetById(Guid id)
        {
            var result = _rep.GetById(id);
            return result;
        }
        public DoPolicy checkExistMemberGroup(DoPolicy entity)
        {
            return _rep.Table.FirstOrDefault(c => c.MemberGroupId == entity.MemberGroupId);
        }
        public DoPolicy CheckingForUpdate(DoPolicy entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && !int
            .Equals(c.BookNumber, entity.BookNumber));
        }

        #endregion
    }
}