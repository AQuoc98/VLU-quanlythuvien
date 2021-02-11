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
        private Mock<IRepository<CoreConfigGroup>> _coreConfigGroupRep = new Mock<IRepository<CoreConfigGroup>>();
        private Mock<IRepository<CoreConfig>> _rep = new Mock<IRepository<CoreConfig>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();
        private Mock<IRepository<CoreUser>> _userRep = new Mock<IRepository<CoreUser>>();

        public CoreConfigTest()
        {

        }

        [Fact]
        public void GetConfigGroup_HappyCase_ReturnValues()
        {
            // Arrange
            var response = new List<CoreConfigGroup> {
                    new CoreConfigGroup {

                    }
                };
            _coreConfigGroupRep.Setup(x => x.GetAll())
                .Returns(response);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);
            CoreConfigDm coreConfigDm = new CoreConfigDm(_rep.Object, _coreConfigGroupRep.Object, _httpContextAccessor.Object, _userRep.Object);
            // Act
            var result = coreConfigDm.GetConfigGroup();
            // Assert
            Assert.NotNull(result);
        }

    }
}
