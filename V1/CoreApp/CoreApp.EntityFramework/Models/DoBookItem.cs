using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.EntityFramework.Models
{
    public partial class DoBookItem 
    {
        public DoBookItem()
        {
            DoBookLendings = new HashSet<DoBookLending>();
            DoReservationBooks = new HashSet<DoReservationBook>();
        }

        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Barcode { get; set; }
        public bool IsReferenceOnly { get; set; }
        public bool IsRareBook { get; set; }
        public int Price { get; set; }
        [MaxLength(4)]
        public string PublicationYear { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? RackId { get; set; }
        public Guid? FormatId { get; set; }
        public Guid? BookId { get; set; }
        public Guid? StatusId { get; set; }
        public virtual DoStatus Status { get; set; }
        public virtual DoRack Rack { get; set; }
        public virtual DoFormat Format { get; set; }
        public virtual DoBook Book { get; set; }
        public Guid? BookLendingId { get; set; }
        public virtual DoBookLending BookLending { get; set; }
        public virtual ICollection<DoBookLending> DoBookLendings { get; set; }
        public virtual ICollection<DoReservationBook> DoReservationBooks { get; set; }
    }
}
