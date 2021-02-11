using System;

namespace CoreApp.Common.Models
{
    public class GridViewSelect
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ColumnId { get; set; }
        public string ColumnSqlName { get; set; }
    }
}
