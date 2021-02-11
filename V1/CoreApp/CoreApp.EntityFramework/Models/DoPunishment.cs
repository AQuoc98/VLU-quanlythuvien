using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.EntityFramework.Models
{
  public partial  class DoPunishment
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Reason { get; set;}
        public DateTime? PunishDate { get; set; }

        public int pricePunishment { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? MemberId { get; set; }
         public virtual DoMember Member { get; set; }
    }
}
