using CoreApp.Domain.Base;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CoreApp.Domain.Systems.Implementations
{
    public sealed class CoreTableDm : BaseDomain<CoreTable>, ICoreTableDm
    {
        #region Fields
        private readonly IRepository<CoreDataType> _coreDataTypeRep;
        private readonly IRepository<CoreColumn> _coreColumnRep;
        #endregion

        #region Contructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="rep"></param>
        public CoreTableDm(IRepository<CoreTable> rep, IRepository<CoreDataType> coreDataTypeRep, IRepository<CoreColumn> coreColumnRep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep) : base(rep, httpContextAccessor, userRep)
        {
            _coreDataTypeRep = coreDataTypeRep;
            _coreColumnRep = coreColumnRep;
        }
        #endregion

        #region Utilities

        #endregion

        #region Methods

        public override CoreTable GetById(Guid id)
        {
            return _rep.Include(i => i.CoreColumns).Single(s => s.Id == id);
        }

        public IList<CoreDataType> GetDataType()
        {
            return _coreDataTypeRep.GetAll();
        }

        public override int Update(CoreTable entity)
        {
            var columnAddList = entity.CoreColumns.Where(p => p.Id == Guid.Empty).ToList();
            var columnUpdateList = entity.CoreColumns.Where(p => p.Id != Guid.Empty).ToList();
            var columnUpdateIds = columnUpdateList.Select(s => s.Id).ToArray();
            var columnDeleteList = _coreColumnRep.Table.Where(p => p.TableId == entity.Id && !columnUpdateIds.Contains(p.Id)).ToList();

            _coreColumnRep.Insert(columnAddList);
            _coreColumnRep.Update(columnUpdateList);
            _coreColumnRep.Delete(columnDeleteList);

            _rep.Update(entity);

            return SaveChanges();
        }

        public bool CheckAliasExisted(CoreTable entity)
        {
            return _rep.Table.Any(a => a.Alias == entity.Alias && a.Id != entity.Id);
        }
        #endregion
    }
}
