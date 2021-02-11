using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Common.Models
{
    public class ViewSearchConditionModel
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public string Condition { get; set; }
        public string Operator { get; set; }
        public Guid ViewId { get; set; }
        public string Value { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
