using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Service.Base;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Authentication.Jwt;
using System.Collections.Generic;
using System;

namespace CoreApp.Service.Systems.Implementations
{
    public sealed class CoreEnumService : BaseService<CoreEnum>, ICoreEnumService
    {
        #region Fields
        private readonly ICoreEnumDm _dm;
        #endregion

        #region Contructors
        public CoreEnumService(ICoreEnumDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion

        #region Utilities
        #endregion

        #region Methods
        public IList<CoreEnumValue> GetEnumValuesByEnum(string enumCode)
        {
            return _dm.GetEnumValuesByEnum(enumCode);
        }

        public IList<CoreEnumValue> GetEnumValuesByEnumId(Guid enumId)
        {
            return _dm.GetEnumValuesByEnumId(enumId);
        }

        public CoreEnumValue GetEnumValuesById(Guid Id)
        {
            return _dm.GetEnumValuesById(Id);
        }

        public CoreEnumValue GetEnumValuesByCode(string enumCode, string enumValueCode)
        {
            return _dm.GetEnumValuesByCode(enumCode, enumValueCode);
        }
        #endregion
    }
}
