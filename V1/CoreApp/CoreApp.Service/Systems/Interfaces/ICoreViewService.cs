using CoreApp.Service.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.Common.Models;
using System;
using System.Collections.Generic;

namespace CoreApp.Service.Systems.Interfaces
{
    public interface ICoreViewService : IBaseService<CoreView>
    {
        QueryDataModel QueryData(QueryDataModel model);
        SearchDataModel SearchData(SearchDataModel model);
        IList<CoreViewColumn> GetViewDetails(Guid moduleId);
        IList<CoreView> GetViewsByModule(Guid moduleId);
        GridViewFilterControl GetGridViewFilterSelectData(Guid moduleId);
    }
}
