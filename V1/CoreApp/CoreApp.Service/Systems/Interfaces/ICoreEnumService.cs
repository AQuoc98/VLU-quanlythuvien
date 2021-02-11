using CoreApp.Service.Base;
using CoreApp.EntityFramework.Models;
using System.Collections.Generic;
using System;

namespace CoreApp.Service.Systems.Interfaces
{
    public interface ICoreEnumService : IBaseService<CoreEnum>
    {
        IList<CoreEnumValue> GetEnumValuesByEnum(string enumCode);
        IList<CoreEnumValue> GetEnumValuesByEnumId(Guid enumId);
        CoreEnumValue GetEnumValuesById(Guid Id);
        CoreEnumValue GetEnumValuesByCode(string enumCode, string enumValueCode);
    }
}
