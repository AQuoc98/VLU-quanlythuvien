using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;

namespace CoreApp.Domain.Systems.Interfaces
{
    public interface ICoreUserDm : IBaseDomain<CoreUser>
    {
        CoreCredentialType GetCredentialTypeByCode(string code);
        CoreCredential GetCredential(string identifier, string secret, Guid credentialTypeId);
        IList<CoreRole> GeRolesByUserId(Guid userId);
        CoreCredential GetCredentialByIdentifier(string identifier);
        CoreCredential CheckAccountExisted(CoreCredential entity);
        CoreUser CheckPhoneExisted(CoreUser entity);
        CoreUser CheckingForEmail(CoreUser entity);
        CoreUser CheckingForName(CoreUser entity);
        CoreUser CheckingForUpdate(CoreUser entity);
        CoreUser GetUserByIdentifier(string identifier);
        CoreUser GetCurrentUser();
        int ChangePassword(ChangePasswordVM entity);
    }
}
