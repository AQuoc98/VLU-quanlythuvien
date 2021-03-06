﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace CoreApp.EntityFramework.Models
{
    public partial class CoreModule
    {
        public CoreModule()
        {
            CoreRoleModulePermissions = new HashSet<CoreRoleModulePermission>();
            CoreTables = new HashSet<CoreTable>();
            CoreViews = new HashSet<CoreView>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActived { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<CoreRoleModulePermission> CoreRoleModulePermissions { get; set; }
        public virtual ICollection<CoreTable> CoreTables { get; set; }
        public virtual ICollection<CoreView> CoreViews { get; set; }
    }
}