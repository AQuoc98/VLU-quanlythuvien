using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.EntityFramework.Models
{
    public partial class DoFormat
    {
        public DoFormat()
        {
            DoBookItems = new HashSet<DoBookItem>();
        }

        [Key]
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<DoBookItem> DoBookItems { get; set; }
    }
}
