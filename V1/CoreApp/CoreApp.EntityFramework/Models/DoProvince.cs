﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace CoreApp.EntityFramework.Models
{
    public partial class DoProvince
    {
        public DoProvince()
        {
            DoDistricts = new HashSet<DoDistrict>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual ICollection<DoDistrict> DoDistricts { get; set; }
    }
}