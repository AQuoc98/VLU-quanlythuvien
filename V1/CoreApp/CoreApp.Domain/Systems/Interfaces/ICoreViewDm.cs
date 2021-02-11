using CoreApp.Common.Models;
using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Interfaces
{
    public interface ICoreViewDm : IBaseDomain<CoreView>
    {
        IList<object> QueryData(QueryDataModel model);
        IList<object> SearchData(SearchDataModel model);
        IList<CoreViewColumn> GetViewDetails(Guid viewId, Guid moduleId);
        IList<CoreView> GetViewsByModule(Guid moduleId);
        GridViewFilterControl GetGridViewFilterSelectData(Guid moduleId);
    }
}
