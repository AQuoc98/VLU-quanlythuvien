using CoreApp.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace CoreApp.Authentication.Authorizations
{
    public class ModuleBasedAuthorizeAttribute : AuthorizeAttribute
    {
        #region Fields
        private readonly string _module;
        private readonly string _permission;
        #endregion

        #region Contructors
        public ModuleBasedAuthorizeAttribute(string module, string permission)
        {
            _module = module;
            _permission = permission;
            Policy = $"{JwtConstants.Authorizations.ModuleBasedPolicyPrefix}{_module}_{_permission}";
        }
        #endregion

        #region Properties
        //public string Module
        //{
        //    get
        //    {
        //        return Policy.Substring(JwtConstants.Authorizations.ModuleBasedPolicyPrefix.Length, _module.Length);
        //    }
        //}
        //public string Permission
        //{
        //    get
        //    {
        //        return Policy.Substring(JwtConstants.Authorizations.ModuleBasedPolicyPrefix.Length + _module.Length + 1);
        //    }
        //}
        #endregion
    }
}
