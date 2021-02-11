using CoreApp.Common.Constants;
using CoreApp.Common.Helpers;
using CoreApp.EntityFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Authentication.Authorizations
{
    public class ModuleBasedAuthorizationHandler : AuthorizationHandler<ModuleBasedRequirement>
    {
        #region Fields
        private readonly ClaimsPrincipal _caller;
        #endregion

        #region Contructors
        public ModuleBasedAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
        }
        #endregion

        #region Methods
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ModuleBasedRequirement requirement)
        {

            // Get modules form claims
            var modulesClaim = _caller.Claims.FirstOrDefault(c => c.Type == requirement.Module);
            if (modulesClaim != null)
            {
                var permissions = JsonHelper.Deserialize<string[]>(modulesClaim.Value);

                // Check permission
                if (permissions.Any(code => code == requirement.Permission))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
