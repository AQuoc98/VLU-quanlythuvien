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
    public class TypeOfReaderTest
    {
        private Mock<IRepository<DoMemberGroup>> _rep = new Mock<IRepository<DoMemberGroup>>();
        private Mock<IRepository<DoMemberGroupService>> _doMemberGroupS = new Mock<IRepository<DoMemberGroupService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public TypeOfReaderTest()
        {

        }

        [Fact]
        public List<DoMemberGroup> GetAlls()
        {
            //Arrange
            var response = new List<DoMemberGroup>() {
                new DoMemberGroup
                {
                    Name = "Student",
                    Description = "Day la Student"
                }
            };

            var response1 = new List<DoMemberGroup>() {
                new DoMemberGroup
                {
                    Name = "Teacher",
                    Description = "Day la Teacher"
                }
            };

            var response2 = new List<DoMemberGroup>() {
                new DoMemberGroup
                {
                    Name = "Student 1",
                    Description = "Day la Student 1"
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _doMemberGroupS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By ID.
        [Fact]
        public List<DoMemberGroup> GetByIds()
        {
            //Arrange
            var getbyid = new List<DoMemberGroup>() {
                new DoMemberGroup
                {
                    Name =  "Student",
                    Description = "Day la Student"
                }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _doMemberGroupS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoMemberGroup> Inserts()
        {
            //Arrange
            var inserts = new List<DoMemberGroup>() {
                new DoMemberGroup
                {
                    Name = "Hoc sinh",
                    Description = "Day la Hoc sinh"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doMemberGroupS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoMemberGroup> Updates()
        {
            //Arrange
            var update = new List<DoMemberGroup>() {
                new DoMemberGroup
                {
                    Name = "Sinh Vien",
                    Description = "Day la Sinh Vien"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doMemberGroupS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoMemberGroup> Delete()
        {
            //Arrange
            var del = new List<DoMemberGroup>() {
                new DoMemberGroup
                {
                    Name = "Giang Vien",
                    Description = "Day la Giang Vien"
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _doMemberGroupS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
