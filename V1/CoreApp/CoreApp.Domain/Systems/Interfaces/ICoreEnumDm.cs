using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Interfaces
{
    public interface ICoreEnumDm : IBaseDomain<CoreEnum>
    {
        IList<CoreEnumValue> GetEnumValuesByEnum(string enumCode);
        IList<CoreEnumValue> GetEnumValuesByEnumId(Guid enumId);
        CoreEnumValue GetEnumValuesById(Guid Id);
        CoreEnumValue GetEnumValuesByCode(string enumCode, string enumValueCode);
    }
}
