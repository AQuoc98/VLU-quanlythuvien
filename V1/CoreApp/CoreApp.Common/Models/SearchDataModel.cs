using System.Collections.Generic;

namespace CoreApp.Common.Models
{
    public class SearchDataModel
    {
        public QueryDataModel QueryDataModel { get; set; }
        public IList<ViewSearchConditionModel> SearchConditions { get; set; }
    }
}
