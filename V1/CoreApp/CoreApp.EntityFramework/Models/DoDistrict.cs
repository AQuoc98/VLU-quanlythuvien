﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace CoreApp.EntityFramework.Models
{
    public partial class DoDistrict
    {
        public DoDistrict()
        {
            DoWards = new HashSet<DoWard>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public Guid? ProvinceId { get; set; }
        public string ProviceCode { get; set; }
        public string Code { get; set; }

        public virtual DoProvince Province { get; set; }
        public virtual ICollection<DoWard> DoWards { get; set; }
    }
}