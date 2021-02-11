using CoreApp.Domain.Base;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Implementations
{
    public sealed class CoreConfigDm : BaseDomain<CoreConfig>, ICoreConfigDm
    {
        #region Fields
        private readonly IRepository<CoreConfigGroup> _coreConfigGroupRep;
        #endregion

        #region Contructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="rep"></param>
        public CoreConfigDm(IRepository<CoreConfig> rep, IRepository<CoreConfigGroup> coreConfigGroupRep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep) : base(rep, httpContextAccessor, userRep)
        {
            _coreConfigGroupRep = coreConfigGroupRep;
        }
        #endregion

        #region Utilities

        #endregion

        #region Methods
        public IList<CoreConfigGroup> GetConfigGroup()
        {
            return _coreConfigGroupRep.GetAll();
        }
        #endregion
    }
}
