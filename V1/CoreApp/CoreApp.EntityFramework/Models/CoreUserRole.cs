﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace CoreApp.EntityFramework.Models
{
    public partial class CoreUserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual CoreRole Role { get; set; }
        public virtual CoreUser User { get; set; }
    }
}