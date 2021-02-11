using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CoreApp.EntityFramework.Models
{
    public partial class DoRack
    {
        public DoRack()
        {
            DoBookItems = new HashSet<DoBookItem>();
        }
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Number { get; set; }
        [MaxLength(500)]
        public string LocationIndentifier { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<DoBookItem> DoBookItems { get; set; }
    }
}