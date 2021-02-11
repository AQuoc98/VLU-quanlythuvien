using CoreApp.Domain.Systems.Implementations;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoreApp.DomainTest.SystemsTests
{
    public class CoreConfigTest
    {
        private MockIRepositoryCoreConfigGroup _coreConfigGroupRep = new MockIRepositoryCoreConfigGroup();
        private MockIRepositoryCoreConfig _rep = new MockIRepositoryCoreConfig();
        private MockIHttpContextAccessor _httpContextAccessor = new MockIHttpContextAccessor();
        private MockIRepositoryCoreUser _userRep = new MockIRepositoryCoreUser();

        public CoreConfigTest()
        {

        }

        [Fact]
        public void GetConfigGroup_HappyCase_ReturnValues()
        {
            Arrange
           var response = new ListCoreConfigGroup {
                    new CoreConfigGroup {

                    }
               };
            _coreConfigGroupRep.Setup(x = x.GetAll())
                .Returns(response);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x = x.HttpContext).Returns(ctx.HttpContext);
            CoreConfigDm coreConfigDm = new CoreConfigDm(_rep.Object, _coreConfigGroupRep.Object, _httpContextAccessor.Object, _userRep.Object);
            Act
           var result = coreConfigDm.GetConfigGroup();
            Assert
            Assert.NotNull(result);
        }

    }
}
