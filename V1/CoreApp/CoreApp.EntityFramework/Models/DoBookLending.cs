using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.EntityFramework.Models
{
  public partial  class DoBookLending
    {
        public DoBookLending()
        {
            DoBookItems = new HashSet<DoBookItem>();
           
        }
        [Key]
        public Guid Id { get; set; }
         public DateTime BorrowedDate { get; set; }
        public DateTime? DueDate { get; set; }
         public DateTime? ReturnDate { get; set; }
 
        [MaxLength(50)]
        public string Note { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? BookItemId { get; set; }
        public virtual DoBookItem BookItem { get; set; }
        public Guid? MemberId { get; set; }
        public virtual DoMember Member { get; set; }

        public virtual ICollection<DoBookItem> DoBookItems { get; set; }
        
    }
}
