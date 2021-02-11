using Microsoft.AspNetCore.Authorization;

namespace CoreApp.Authentication.Authorizations
{
    public class ModuleBasedRequirement : IAuthorizationRequirement
    {
        #region Contructors
        public ModuleBasedRequirement(string module, string permission)
        {
            Module = module;
            Permission = permission;
        }
        #endregion

        #region Properties
        public string Module { get; private set; }
        public string Permission { get; private set; }
        #endregion
    }
}
