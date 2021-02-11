using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using CoreApp.EntityFramework.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreApp.EntityFramework.Models
{
    public partial class DoBook
    {
        public DoBook()
        {
            DoBookItems = new HashSet<DoBookItem>();
            DoReservationBooks = new HashSet<DoReservationBook>();
        }
        [Key]
        [Required]
        public Guid Id { get; set; }
        [MaxLength(30)]
        public string ISBN { get; set; }
        [MaxLength(500)]
        public string Title { get; set; }
        [MaxLength(1500)]
        public string Subject { get; set; }

        public Guid? LanguageId { get; set; }
        public int NumberOfPages { get; set; }
        public string Image { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? CatalogId { get; set; }
        public virtual DoCatalog Catalog { get; set; }
        public Guid? PublishierId { get; set; }
        public virtual DoPublishier Publishier { get; set; }
        public virtual DoLanguage Language { get; set; }
        public Guid? AuthorId { get; set; }
        public virtual DoAuthor Author { get; set; }
        public virtual ICollection<DoBookItem> DoBookItems { get; set; }
        public virtual ICollection<DoReservationBook> DoReservationBooks { get; set; }
    }
}
