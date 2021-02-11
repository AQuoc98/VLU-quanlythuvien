using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.EntityFramework.ViewModels
{
    public class ChangePasswordVM
    {
        public string Secret { get; set; }
        public string NewSecret { get; set; }
    }
}
