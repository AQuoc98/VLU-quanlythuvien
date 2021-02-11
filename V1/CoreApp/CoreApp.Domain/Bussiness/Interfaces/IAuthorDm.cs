using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface IAuthorDm : IBaseDomain<DoAuthor>
    {
        new DoAuthor GetById(Guid id);
        DoAuthor CheckingForAuthor(DoAuthor entity);
        DoAuthor CheckingForUpdate(DoAuthor entity);
      
        
    }
}
