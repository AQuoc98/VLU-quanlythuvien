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
    public class LanguageTest
    {
        private Mock<IRepository<DoLanguage>> _rep = new Mock<IRepository<DoLanguage>>();
        private Mock<IRepository<DoLanguageService>> _doLanguageS = new Mock<IRepository<DoLanguageService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public LanguageTest()
        {

        }

        [Fact]
        public List<DoLanguage> GetAlls()
        {
            //Arrange
            var response = new List<DoLanguage>() {
                new DoLanguage
                {
                    Name = "Việt Nam",
                    Description = "Đây là ngôn ngữ Việt Nam"
                }
            };

            var response1 = new List<DoLanguage>() {
                new DoLanguage
                {
                    Name = "Tiếng Anh",
                    Description = "Đây là ngôn ngữ Tiếng Anh"
                }
            };

            var response2 = new List<DoLanguage>() {
                new DoLanguage
                {
                    Name = "Ba Lan",
                    Description = "Đây là ngôn ngữ Ba Lan"
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _doLanguageS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By ID.
        [Fact]
        public List<DoLanguage> GetByIds()
        {
            //Arrange
            var getbyid = new List<DoLanguage>() {
                new DoLanguage
                {
                    Name =  "Nga",
                    Description = "Đây là ngôn ngữ Nga"
                }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _doLanguageS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoLanguage> Inserts()
        {
            //Arrange
            var inserts = new List<DoLanguage>() {
                new DoLanguage
                {
                    Name = "Hàn Quốc",
                    Description = "Đây là ngôn ngữ Hàn Quốc"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doLanguageS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoLanguage> Updates()
        {
            //Arrange
            var update = new List<DoLanguage>() {
                new DoLanguage
                {
                    Name = "Thái Lan",
                    Description = "Đây là ngôn ngữ Thái Lan"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doLanguageS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoLanguage> Delete()
        {
            //Arrange
            var del = new List<DoLanguage>() {
                new DoLanguage
                {
                    Name = "Pháp",
                    Description = "Đây là ngôn ngữ Pháp"
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _doLanguageS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
