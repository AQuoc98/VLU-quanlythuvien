using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Service.Base;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Authentication.Jwt;
using CoreApp.Common.Models;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CoreApp.Service.Systems.Implementations
{
    public sealed class CoreViewService : BaseService<CoreView>, ICoreViewService
    {
        #region Fields
        private readonly ICoreViewDm _dm;
        #endregion

        #region Contructors
        public CoreViewService(ICoreViewDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion

        #region Utilities
        #endregion

        #region Methods
        /// <summary>
        /// Query Dynamic Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public QueryDataModel QueryData(QueryDataModel model)
        {
            model.UserId = CurrentUserId;
            model.Data = _dm.QueryData(model);
            return model;
        }

        /// <summary>
        /// Search Dynamic Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SearchDataModel SearchData(SearchDataModel model)
        {
            model.QueryDataModel.UserId = CurrentUserId;
            foreach (var cond in model.SearchConditions)
            {
                SetAuditFields(cond);
            }
            model.QueryDataModel.Data = _dm.SearchData(model);
            return model;
        }

        public IList<CoreViewColumn> GetViewDetails(Guid moduleId)
        {
            return _dm.GetViewDetails(Guid.Empty, moduleId);
        }

        public IList<CoreView> GetViewsByModule(Guid moduleId)
        {
            return _dm.GetViewsByModule(moduleId);
        }

        public GridViewFilterControl GetGridViewFilterSelectData(Guid moduleId)
        {
            return _dm.GetGridViewFilterSelectData(moduleId);
        }
        #endregion
    }
}
