﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace CoreApp.EntityFramework.Models
{
    public partial class CorePermission
    {
        public CorePermission()
        {
            CoreRoleModulePermissions = new HashSet<CoreRoleModulePermission>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameDict { get; set; }
        public int Position { get; set; }

        public virtual ICollection<CoreRoleModulePermission> CoreRoleModulePermissions { get; set; }
    }
}