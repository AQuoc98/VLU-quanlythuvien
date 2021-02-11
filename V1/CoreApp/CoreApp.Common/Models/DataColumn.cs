using System;

namespace CoreApp.Common.Models
{
    public class DataColumn
    {
        public Guid Id { get; set; }
        public string SqlName { get; set; }
        public string Name { get; set; }
        public string NameDict { get; set; }
        public bool Searchable { get; set; }
        public bool Sortable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public Guid? EnumId { get; set; }
        public string DataTypeCode { get; set; }
        public int Position { get; set; }
        public bool Visible { get; set; }
        public string Width { get; set; }
        public string TableAlias { get; set; }

        internal object Select()
        {
            throw new NotImplementedException();
        }
    }
}
