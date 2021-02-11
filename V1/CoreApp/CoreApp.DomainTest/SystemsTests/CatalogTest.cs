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
    public class CatalogTest
    {
        private Mock<IRepository<DoCatalog>> _rep = new Mock<IRepository<DoCatalog>>();
        private Mock<IRepository<DoCatalogService>> _doCatalogS = new Mock<IRepository<DoCatalogService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public CatalogTest()
        {

        }

        [Fact]
        public List<DoCatalog> GetAlls()
        {
            //Arrange
            var response = new List<DoCatalog>() {
                new DoCatalog
                {
                    Name = "Khoa hoc",
                    Description = "Day la sach Khoa hoc"
                }
            };

            var response1 = new List<DoCatalog>() {
                new DoCatalog
                {
                    Name = "Khoa hoc chuyen sau",
                    Description = "Day la sach Khoa hoc chuyen sau"
                }
            };

            var response2 = new List<DoCatalog>() {
                new DoCatalog
                {
                    Name = "Khoa hoc chuyen sau",
                    Description = "Day la sach Khoa hoc chuyen sau"
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _doCatalogS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By ID.
        [Fact]
        public List<DoCatalog> GetByIds()
        {
            //Arrange
            var getbyid = new List<DoCatalog>() {
                new DoCatalog
                {
                    Name =  "Sach Van Hoc",
                    Description = "Day la sach van hoc"
                }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _doCatalogS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoCatalog> Inserts()
        {
            //Arrange
            var inserts = new List<DoCatalog>() {
                new DoCatalog
                {
                    Name = "Sach thien van hoc",
                    Description = "Day la sach thien van hoc"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doCatalogS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoCatalog> Updates()
        {
            //Arrange
            var update = new List<DoCatalog>() {
                new DoCatalog
                {
                    Name = "Tieu thuyet",
                    Description = "Day la tieu thuyet"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doCatalogS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoCatalog> Delete()
        {
            //Arrange
            var del = new List<DoCatalog>() {
                new DoCatalog
                {
                    Name = "Tin hoc",
                    Description = "Day la sach tin hoc"
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _doCatalogS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
