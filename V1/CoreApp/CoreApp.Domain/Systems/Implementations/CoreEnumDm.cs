using CoreApp.Common.Constants;
using CoreApp.Common.Helpers;
using CoreApp.Domain.Base;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreApp.Domain.Systems.Implementations
{
    public sealed class CoreEnumDm : BaseDomain<CoreEnum>, ICoreEnumDm
    {
        #region Fields
        private readonly IRepository<CoreEnumValue> _coreEnumValueRep;
        #endregion

        #region Contructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="rep"></param>
        public CoreEnumDm(IRepository<CoreEnum> rep, IRepository<CoreEnumValue> coreEnumValueRep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep) : base(rep, httpContextAccessor, userRep)
        {
            _coreEnumValueRep = coreEnumValueRep;
        }
        #endregion

        #region Utilities

        #endregion

        #region Methods
        public override CoreEnum GetById(Guid id)
        {
            return _rep.Include(i => i.CoreEnumValues).Single(s => s.Id == id);
        }

        public override IList<CoreEnum> GetAll()
        {
            return _rep.Table.Where(p => !p.Invisible).ToList();
        }

        public override int Insert(CoreEnum entity)
        {
            foreach (var item in entity.CoreEnumValues)
            {
                // Generate slug 
                var slug = UrlHelper.GenerateSlug(item.Name);
                var slugExistCount = _coreEnumValueRep.Table.Count(p => p.Slug == slug);
                if (slugExistCount > 0)
                {
                    slug = $"{slug}-{slugExistCount}";
                }
                item.Slug = slug;
            }

            return base.Insert(entity);
        }

        public override int Update(CoreEnum entity)
        {
            // enumValues
            var enumValueAddList = entity.CoreEnumValues.Where(p => p.Id == Guid.Empty).ToList();
            var enumValueUpdateList = entity.CoreEnumValues.Where(p => p.Id != Guid.Empty).ToList();
            var enumValueUpdateIds = enumValueUpdateList.Select(s => s.Id).ToArray();
            var enumValueDeleteList = _coreEnumValueRep.Table.Where(p => !enumValueUpdateIds.Contains(p.Id) && p.EnumId == entity.Id).ToList();

            foreach (var item in enumValueAddList)
            {
                // Generate slug 
                var slug = UrlHelper.GenerateSlug(item.Name);
                var slugExistCount = _coreEnumValueRep.Table.Count(p => p.Slug == slug);
                if (slugExistCount > 0)
                {
                    slug = $"{slug}-{slugExistCount}";
                }
                item.Slug = slug;
            }
            _coreEnumValueRep.Insert(enumValueAddList);

            foreach (var item in enumValueUpdateList)
            {
                // Generate slug 
                var slug = UrlHelper.GenerateSlug(item.Name);
                var slugExistCount = _coreEnumValueRep.Table.Count(p => p.Slug == slug && p.Id != entity.Id);
                if (slugExistCount > 0)
                {
                    slug = $"{slug}-{slugExistCount}";
                }
                item.Slug = slug;
            }
            _coreEnumValueRep.Update(enumValueUpdateList);

            _coreEnumValueRep.Delete(enumValueDeleteList);

            _rep.Update(entity);

            return SaveChanges();
        }

        public IList<CoreEnumValue> GetEnumValuesByEnum(string enumCode)
        {
            var enumEntity = _rep.Table.SingleOrDefault(s => s.Code == enumCode);
            return _coreEnumValueRep.Table.Where(p => p.EnumId == enumEntity.Id).ToList();
        }

        public IList<CoreEnumValue> GetEnumValuesByEnumId(Guid enumId)
        {
            return _coreEnumValueRep.Table.Where(p => p.EnumId == enumId).ToList();
        }

        public CoreEnumValue GetEnumValuesById(Guid Id)
        {
            return _coreEnumValueRep.Table.Single(p => p.Id == Id);
        }

        public CoreEnumValue GetEnumValuesByCode(string enumCode, string enumValueCode)
        {
            return _coreEnumValueRep.Include(x => x.Enum).SingleOrDefault(p => p.Enum.Code == enumCode && p.Code == enumValueCode);
        }
        #endregion
    }
}
