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
    public sealed class DoRackDm : BaseDomain<DoRack>, IRackDm
    {
        public DoRackDm(IRepository<DoRack> rep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep)
            : base(rep, httpContextAccessor, userRep)
        {
        }
        #region
        public override DoRack GetById(Guid id)
        {
            var result = _rep.GetById(id);
            return result;
        }
        public DoRack CheckingForLocation(DoRack entity)
        {
            return _rep.Table.FirstOrDefault(c => c.LocationIndentifier == entity.LocationIndentifier && !string.IsNullOrEmpty(entity.LocationIndentifier) && string
            .Equals(c.LocationIndentifier, entity.LocationIndentifier, StringComparison.OrdinalIgnoreCase));
        }
        public DoRack CheckingForNumber(DoRack entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && int.Equals(c.Number,entity.Number));
        }
        public DoRack CheckingForUpdate(DoRack entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && !string.IsNullOrEmpty(entity.LocationIndentifier) && string
            .Equals(c.LocationIndentifier, entity.LocationIndentifier, StringComparison.OrdinalIgnoreCase));
        }

        public override int Delete(IEnumerable<DoRack> entities)
        {
            var RackIdsDelete = entities.Select(x => x.Id).ToArray();
            return base.Delete(entities);
        }
        #endregion
    }
}