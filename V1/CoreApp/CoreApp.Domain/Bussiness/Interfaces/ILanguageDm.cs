using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
namespace CoreApp.Domain.Bussiness.Interfaces
{
    public interface ILanguageDm : IBaseDomain<DoLanguage>
    {
        new DoLanguage GetById(Guid id);
        DoLanguage CheckingForLanguage(DoLanguage entity);
        DoLanguage CheckingForUpdate(DoLanguage entity);
    }
}
