using CoreApp.Common.Constants;
using CoreApp.Domain.Base;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CoreApp.Domain.Systems.Implementations
{
    public sealed class CoreMenuDm : BaseDomain<CoreMenu>, ICoreMenuDm
    {
        #region Fields
        #endregion

        #region Contructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="rep"></param>
        public CoreMenuDm(IRepository<CoreMenu> rep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep) : base(rep, httpContextAccessor, userRep)
        {
            
        }
        #endregion

        #region Utilities

        #endregion

        #region Methods
        public IList<CoreMenu> GetMenus(Guid userId)
        {
            var userIdParam = new SqlParameter
            {
                ParameterName = "userId",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Value = userId
            };
            return _rep.ExcuteStoreProcedure<CoreMenu>(SqlStoreProcedureConstant.SpGetMenus, userIdParam);
        }

        public IList<CoreMenu> GetParentMenus()
        {
            return _rep.Table.Where(p => p.ParentId == null).ToList();
        }
        #endregion
    }
}
