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
    public sealed class DoMemberDm : BaseDomain<DoMember>, IMemberDm
    {

        public DoMemberDm(IRepository<DoMember> rep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep)
            : base(rep, httpContextAccessor, userRep)
        {

        }
        #region
        public override DoMember GetById(Guid id)
        {
            var result = _rep.Include(r => r.MemberGroup).AsNoTracking().Single(a => a.Id == id);
            return result;
        }
        public DoMember CheckingForExistMemberCode(DoMember entity)
        {
            return _rep.Table.FirstOrDefault(c => c.MemberCode == entity.MemberCode && !string.IsNullOrEmpty(entity.MemberCode) && string
            .Equals(c.MemberCode, entity.MemberCode, StringComparison.OrdinalIgnoreCase));
        }
        public DoMember CheckingUpdateExistMemberCode(DoMember entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && !string.IsNullOrEmpty(entity.MemberCode) && string
            .Equals(c.MemberCode, entity.MemberCode, StringComparison.OrdinalIgnoreCase));
        }


        #endregion
    }
}