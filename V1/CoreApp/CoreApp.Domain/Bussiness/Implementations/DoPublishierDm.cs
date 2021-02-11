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
    public sealed class DoPublishierDm : BaseDomain<DoPublishier>, IPublishierDm
    {
        public DoPublishierDm(IRepository<DoPublishier> rep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep)
            : base(rep, httpContextAccessor, userRep)
        {
        }
        #region
        public override DoPublishier GetById(Guid id)
        {
            var result = _rep.GetById(id);
            return result;
        }
        public DoPublishier CheckingForName(DoPublishier entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Name == entity.Name && !string.IsNullOrEmpty(entity.Name) && string
            .Equals(c.Name, entity.Name, StringComparison.OrdinalIgnoreCase));
        }
        public DoPublishier CheckingForUpdate(DoPublishier entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && !string.IsNullOrEmpty(entity.Name) && string
            .Equals(c.Name, entity.Name, StringComparison.OrdinalIgnoreCase));
        }
        public override int Delete(IEnumerable<DoPublishier> entities)
        {
            var PubIdsDelete = entities.Select(x => x.Id).ToArray();
            return base.Delete(entities);
        }
        #endregion
    }
}