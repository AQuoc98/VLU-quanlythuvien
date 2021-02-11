using CoreApp.Authentication.Jwt;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using CoreApp.Service.Bussiness.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;
using System;

namespace CoreApp.DomainTest.SystemsTests
{
    public class BookRuleBorrowingTest
    {
        private Mock<IRepository<DoPolicy>> _rep = new Mock<IRepository<DoPolicy>>();
        private Mock<IRepository<DoPolicyService>> _DoPolicyS = new Mock<IRepository<DoPolicyService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public BookRuleBorrowingTest()
        {

        }

        [Fact]
        public List<DoPolicy> GetAlls()
        {
            System.Guid c628fe6df428d8748e15b75f802a9610 = default;
            System.Guid e8aea2bde30a4d1a94ff6d557f766c8b = default;
            System.Guid dacbddb62c5a41ad878d5b248eafd3fd = default;

            //Arrange
            var response = new List<DoPolicy>() {
                new DoPolicy
                {
                    BookNumber = 12,
                    numberOfDueDate = 14,
                    MemberGroupId = c628fe6df428d8748e15b75f802a9610
                }
            };

            var response1 = new List<DoPolicy>() {
                new DoPolicy
                {
                    BookNumber = 49,
                    numberOfDueDate = 15,
                    MemberGroupId = e8aea2bde30a4d1a94ff6d557f766c8b
                }
            };

            var response2 = new List<DoPolicy>() {
                new DoPolicy
                {
                    BookNumber = 65,
                    numberOfDueDate = 10,
                    MemberGroupId = dacbddb62c5a41ad878d5b248eafd3fd
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _DoPolicyS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By Id.
        [Fact]
        public List<DoPolicy> GetByIds()
        {
            System.Guid eeaeaa170de2b482786f30814e4f4afe4 = default;

            //Arrange
            var getbyid = new List<DoPolicy>() {
                new DoPolicy
                {
                    BookNumber = 432,
                    numberOfDueDate = 11,
                    MemberGroupId = eeaeaa170de2b482786f30814e4f4afe4
                }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _DoPolicyS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoPolicy> Inserts()
        {
            System.Guid b522fff3d18442788ea1e7156154940e = default;

            //Arrange
            var inserts = new List<DoPolicy>() {
                new DoPolicy
                {
                    BookNumber = 90,
                    numberOfDueDate = 10,
                    MemberGroupId = b522fff3d18442788ea1e7156154940e
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _DoPolicyS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoPolicy> Updates()
        {
            System.Guid c7ca4ce1f4781a43fc375e9052a635eb = default;

            //Arrange
            var update = new List<DoPolicy>() {
                new DoPolicy
                {
                    BookNumber = 89,
                    numberOfDueDate = 12,
                    MemberGroupId = c7ca4ce1f4781a43fc375e9052a635eb
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _DoPolicyS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoPolicy> Delete()
        {
            System.Guid c8a93ef4dccc46f698095472e157ef08 = default;

            //Arrange
            var del = new List<DoPolicy>() {
                new DoPolicy
                {
                    BookNumber = 31,
                    numberOfDueDate = 16,
                    MemberGroupId = c8a93ef4dccc46f698095472e157ef08
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _DoPolicyS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
