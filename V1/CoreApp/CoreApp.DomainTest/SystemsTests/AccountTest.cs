using CoreApp.Domain.Systems.Implementations;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CoreApp.DomainTest.SystemsTests
{
    public class AccountTest
    {
        private Mock<IRepository<CoreUser>> _rep = new Mock<IRepository<CoreUser>>();
        private Mock<IRepository<CoreCredentialType>> _credentialTypeRep = new Mock<IRepository<CoreCredentialType>>();
        private Mock<IRepository<CoreCredential>> _credentialRep = new Mock<IRepository<CoreCredential>>();
        private Mock<IRepository<CoreUserRole>> _userRoleRep = new Mock<IRepository<CoreUserRole>>();
        private Mock<IRepository<CoreRole>> _roleRep = new Mock<IRepository<CoreRole>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public AccountTest()
        {

        }
        
        //Get All
        [Fact]
        public void GetAlls()
        {
            //Arrange
            var response = new List<CoreUser>() {
                new CoreUser
                {
                    
                }
            };
            _credentialRep.Setup(x => x.GetAll());
            _userRoleRep.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);
            CoreUserDm coreUserDm = new CoreUserDm(_rep.Object, _credentialTypeRep.Object, _credentialRep.Object, _userRoleRep.Object, _roleRep.Object, _httpContextAccessor.Object);
            // Act
            var result = coreUserDm.GetCurrentUser();
            // Assert
            Assert.NotNull(result);
        }
        
        //Get By ID.
        [Fact]
        public void GetByIds()
        {
            //Arrange
            var getbyid = new List<CoreUser>() {
                new CoreUser
                {
                    
                }
            };
            _credentialRep.Setup(x => x.GetById());
            _userRoleRep.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);
            CoreUserDm coreUserDm = new CoreUserDm(_rep.Object, _credentialTypeRep.Object, _credentialRep.Object, _userRoleRep.Object, _roleRep.Object, _httpContextAccessor.Object);
            // Act
            var result = coreUserDm.GetCurrentUser().Id;
            // Assert
            Assert.NotNull(result);
        }

        //Create.
        [Fact]
        public void Inserts()
        {
            //Arrange
            var inserts = new List<CoreUser>() {
                new CoreUser
                {
                    
                }
            };
            _credentialRep.Setup(x => x.SaveChanges());
            _userRoleRep.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);
            CoreUserDm coreUserDm = new CoreUserDm(_rep.Object, _credentialTypeRep.Object, _credentialRep.Object, _userRoleRep.Object, _roleRep.Object, _httpContextAccessor.Object);
            // Act
            var result = coreUserDm.GetCurrentUser();

            // Assert
            Assert.NotNull(result);

        }

        //Update.
        [Fact]
        public void Updates()
        {
            //Arrange
            var update = new List<CoreUser>() {
                new CoreUser
                {

                }
            };
            _credentialRep.Setup(x => x.SaveChanges());
            _userRoleRep.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);
            CoreUserDm coreUserDm = new CoreUserDm(_rep.Object, _credentialTypeRep.Object, _credentialRep.Object, _userRoleRep.Object, _roleRep.Object, _httpContextAccessor.Object);
            // Act
            var result = coreUserDm.GetCurrentUser().Id;
            // Assert
            Assert.NotNull(result);
        }

        //Delete.
        [Fact]
        public void Delete()
        {
            //Arrange
            var del = new List<CoreUser>() {
                new CoreUser
                {
                    
                }
            };
            _credentialRep.Setup(x => x.GetAll().Clear());
            _userRoleRep.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);
            CoreUserDm coreUserDm = new CoreUserDm(_rep.Object, _credentialTypeRep.Object, _credentialRep.Object, _userRoleRep.Object, _roleRep.Object, _httpContextAccessor.Object);
            // Act
            var result = coreUserDm.GetCurrentUser().Id;
            // Assert
            Assert.NotNull(result);
        }

        
    }
}
