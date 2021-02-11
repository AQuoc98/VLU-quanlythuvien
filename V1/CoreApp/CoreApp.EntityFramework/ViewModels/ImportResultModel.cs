using System;
using System.Collections.Generic;
using System.Text;
using CoreApp.EntityFramework.Models;

namespace CoreApp.EntityFramework.ViewModels
{
   public class ImportResultModel
    {
        public int Success { get; set; }
        public int Total { get; set; }
        public IList<DoBookItem> inValidData { get; set; }

    }
}
