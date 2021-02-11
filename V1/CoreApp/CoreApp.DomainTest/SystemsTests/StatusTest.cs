using CoreApp.Authentication.Jwt;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using CoreApp.Service.Bussiness.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CoreApp.DomainTest.SystemsTests
{
    public class StatusTest
    {
        private Mock<IRepository<DoStatus>> _rep = new Mock<IRepository<DoStatus>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public StatusTest()
        {

        }

        [Fact]
        public List<DoStatus> GetAlls()
        {
            //Arrange
            var response = new List<DoStatus>() {
                new DoStatus
                {
                    Name = "Bị mất",
                    Description = "Bị mất"
                }
            };

            var response1 = new List<DoStatus>() {
                new DoStatus
                {
                    Name = "Sẵn có",
                    Description = "Sẵn có"
                }
            };

            var response2 = new List<DoStatus>() {
                new DoStatus
                {
                    Name = "Đã mượn",
                    Description = "Đã mượn"
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By ID.
        [Fact]
        public List<DoStatus> GetByIds()
        {
            //Arrange
            var getbyid = new List<DoStatus>() {
                new DoStatus
                {
                    Name =  "Bị mất",
                    Description = "Bị mất"
                }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoStatus> Inserts()
        {
            //Arrange
            var inserts = new List<DoStatus>() {
                new DoStatus
                {
                    Name = "Đã mượn",
                    Description = "Đã mượn"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoStatus> Updates()
        {
            //Arrange
            var update = new List<DoStatus>() {
                new DoStatus
                {
                    Name = "Sẵn có",
                    Description = "Sẵn có"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoStatus> Delete()
        {
            //Arrange
            var del = new List<DoStatus>() {
                new DoStatus
                {
                    Name = "Đã mượn",
                    Description = "Đã mượn"
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
