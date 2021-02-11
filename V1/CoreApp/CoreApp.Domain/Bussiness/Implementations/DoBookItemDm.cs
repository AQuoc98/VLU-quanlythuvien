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
    public sealed class DoBookItemDm : BaseDomain<DoBookItem>, IBookitemDm
    {
        public DoBookItemDm(IRepository<DoBookItem> rep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep)
            : base(rep, httpContextAccessor, userRep)
        {
        }

        #region
        public override IList<DoBookItem> GetAll()
        {
            var result = _rep.Include(r => r.Rack, a => a.Format, c => c.Book, x => x.Status).ToList();
            return result;
        }
        public override DoBookItem GetById(Guid id)
        {
            var result = _rep.Include(r => r.Rack, a => a.Format, c => c.Book, x => x.Status)
                .AsNoTracking().Single(b => b.Id == id);
            return result;
        }
        public DoBookItem CheckingForBarcode(DoBookItem entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Barcode == entity.Barcode && !string.IsNullOrEmpty(entity.Barcode) && string
            .Equals(c.Barcode, entity.Barcode, StringComparison.OrdinalIgnoreCase));
        }

        public DoBookItem CheckingForUpdate(DoBookItem entity)
        {
            return _rep.Table.FirstOrDefault(c => c.Id != entity.Id && !string.IsNullOrEmpty(entity.Barcode) && string
            .Equals(c.Barcode, entity.Barcode, StringComparison.OrdinalIgnoreCase));
        }

        public override int Delete(IEnumerable<DoBookItem> entities)
        {
            var CatalogIdsDelete = entities.Select(x => x.Id).ToArray();
            return base.Delete(entities);
        }

        public IList<DoBookItem> GetByBarCode(IEnumerable<string> barCodes)
        {
            return _rep.Table.Where(c => barCodes.Contains(c.Barcode)).ToList();
        }
        #endregion
    }
}
