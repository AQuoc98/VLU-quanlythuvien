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
    public class FormatOfBookItemTest
    {
        private Mock<IRepository<DoFormat>> _rep = new Mock<IRepository<DoFormat>>();
        private Mock<IRepository<DoFormatService>> _doFormatS = new Mock<IRepository<DoFormatService>>();
        private Mock<IRepository<DoBookItem>> _rep1 = new Mock<IRepository<DoBookItem>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public FormatOfBookItemTest()
        {

        }

        [Fact]
        public List<DoFormat> GetAlls()
        {
            //Arrange
            var response = new List<DoFormat>() {
                new DoFormat
                {
                    Name = "Audio Book",
                    Description = "Đây là Audio Book"
                }
            };

            var response1 = new List<DoFormat>() {
                new DoFormat
                {
                    Name = "Ebook",
                    Description = "Đây là Ebook"
                }
            };

            var response2 = new List<DoFormat>() {
                new DoFormat
                {
                    Name = "Tạp chí",
                    Description = "Đây là Tạp chí"
                }
            };
            _rep.Setup(x => x.GetAll());
            _rep1.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _doFormatS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By ID.
        [Fact]
        public List<DoFormat> GetByIds()
        {
            //Arrange
            var getbyid = new List<DoFormat>() {
                new DoFormat
                {
                    Name =  "Tạp chí",
                    Description = "Đây là Tạp chí"
                }
            };
            _rep.Setup(x => x.GetById());
            _rep1.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _doFormatS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoFormat> Inserts()
        {
            //Arrange
            var inserts = new List<DoFormat>() {
                new DoFormat
                {
                    Name = "Bìa cứng",
                    Description = "Đây là bìa cứng",
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _rep1.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doFormatS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoFormat> Updates()
        {
            //Arrange
            var update = new List<DoFormat>() {
                new DoFormat
                {
                    Name = "Bìa mềm",
                    Description = "Đây là bìa mềm"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _rep1.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doFormatS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoFormat> Delete()
        {
            //Arrange
            var del = new List<DoFormat>() {
                new DoFormat
                {
                    Name = "Audio Book",
                    Description = "Đây là Audio Book"
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _rep1.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _doFormatS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
