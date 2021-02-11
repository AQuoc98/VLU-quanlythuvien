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
    public class PublisherTest
    {
        private Mock<IRepository<DoPublishier>> _rep = new Mock<IRepository<DoPublishier>>();
        private Mock<IRepository<DoPublishierService>> _doPublishierS = new Mock<IRepository<DoPublishierService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public PublisherTest()
        {

        }

        [Fact]
        public List<DoPublishier> GetAlls()
        {
            //Arrange
            var response = new List<DoPublishier>() {
                new DoPublishier
                {
                    Name = "NXB Tre",
                    Description = "Day la NXB Tre"
                }
            };

            var response1 = new List<DoPublishier>() {
                new DoPublishier
                {
                    Name = "NXB Giao duc",
                    Description = "Day la NXB Giao duc"
                }
            };

            var response2 = new List<DoPublishier>() {
                new DoPublishier
                {
                    Name = "NXB Kim Dong",
                    Description = "Day la NXB Kim Dong"
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _doPublishierS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By ID.
        [Fact]
        public List<DoPublishier> GetByIds()
        {
            //Arrange
            var getbyid = new List<DoPublishier>() {
                new DoPublishier
                {
                   Name =  "NXB Tong hop TPHCM",
                    Description = "Day la NXB Tong hop TPHCM"
                }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _doPublishierS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoPublishier> Inserts()
        {
            //Arrange
            var inserts = new List<DoPublishier>() {
                new DoPublishier
                {
                    Name = "NXB Chinh tri quoc gia su that",
                    Description = "Day la NXB Chinh tri quoc gia su that"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doPublishierS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoPublishier> Updates()
        {
            //Arrange
            var update = new List<DoPublishier>() {
                new DoPublishier
                {
                    Name = "NXB Tri thuc",
                    Description = "Day la NXB Tri thuc"
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doPublishierS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoPublishier> Delete()
        {
            //Arrange
            var del = new List<DoPublishier>() {
                new DoPublishier
                {
                    Name = "NXB Hong Duc",
                    Description = "Day la NXB Hong Duc"
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _doPublishierS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
