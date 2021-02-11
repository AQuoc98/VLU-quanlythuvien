﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace CoreApp.EntityFramework.Models
{
    public partial class CoreView
    {
        public CoreView()
        {
            CoreViewColumns = new HashSet<CoreViewColumn>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid ModuleId { get; set; }
        public string SelectSql { get; set; }
        public string FromSql { get; set; }
        public string WhereSql { get; set; }
        public string OrderSql { get; set; }
        public bool IsActived { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual CoreModule Module { get; set; }
        public virtual ICollection<CoreViewColumn> CoreViewColumns { get; set; }
    }
}