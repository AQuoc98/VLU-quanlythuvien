using CoreApp.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CoreApp.Authentication.Authorizations
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        #region  Fields
        private readonly AuthorizationOptions _options;
        #endregion

        #region Contructors
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }
        #endregion

        #region Methods

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // Check static policies first
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {
                if (policyName.StartsWith(JwtConstants.Authorizations.ModuleBasedPolicyPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    var modulePermission = policyName.Substring(JwtConstants.Authorizations.ModuleBasedPolicyPrefix.Length).Split('_');
                    var module = modulePermission[0];
                    var permission = modulePermission[1];
                    policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new ModuleBasedRequirement(module, permission))
                    .Build();

                    // Add policy to the AuthorizationOptions, so we don't have to re-create it each time
                    _options.AddPolicy(policyName, policy);
                }
            }

            return policy;
        }
        #endregion
    }
}
