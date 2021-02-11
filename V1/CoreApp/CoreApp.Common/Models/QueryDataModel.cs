using System;
using System.Collections.Generic;

namespace CoreApp.Common.Models
{
    public class QueryDataModel
    {
        public Guid ModuleId { get; set; }
        public Guid ViewId { get; set; }
        public int PageIndex { get; set; }
        public string OrderExpression { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public Guid UserId { get; set; }
        public IList<DataColumn> Columns { get; set; }
        public IList<object> Data { get; set; }
        public string GroupJson { get; set; }
    }
}
