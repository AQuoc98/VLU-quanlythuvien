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
    public sealed class DoBookDm : BaseDomain<DoBook>, IDoBookDm
    {
        private readonly IRepository<DoAuthor> _authorRep;
        private readonly IRepository<DoCatalog> _catalogRep;
        private readonly IRepository<DoPublishier> _publishierRep;
        public DoBookDm(IRepository<DoBook> rep,
            IRepository<DoAuthor> authorRep,
            IRepository<DoCatalog> catalogRep,
            IRepository<DoPublishier> publishierRep,
            IHttpContextAccessor httpContextAccessor,
            IRepository<CoreUser> userRep)
            : base(rep, httpContextAccessor, userRep)
        {
            _authorRep = authorRep;
            _catalogRep = catalogRep;
            _publishierRep = publishierRep;
        }
     
        public override DoBook GetById(Guid id)
        {
            var result = _rep.Include(r => r.Catalog, a => a.Author, c => c.Publishier)
                .AsNoTracking().Single(a => a.Id == id);
            return result;
        }

        public DoBook CheckingForISBN(DoBook entity)
        {
            return _rep.Table.FirstOrDefault(c => c.ISBN == entity.ISBN && !string.IsNullOrEmpty(entity.ISBN) && string
            .Equals(c.ISBN, entity.ISBN, StringComparison.OrdinalIgnoreCase));
        }
        public DoBook CheckingForUpdate(DoBook entity)
        {
            return _rep.Table.FirstOrDefault(c => c.ISBN != entity.ISBN && !string.IsNullOrEmpty(entity.ISBN) && string
           .Equals(c.ISBN, entity.ISBN, StringComparison.OrdinalIgnoreCase));
        }
    }
}
