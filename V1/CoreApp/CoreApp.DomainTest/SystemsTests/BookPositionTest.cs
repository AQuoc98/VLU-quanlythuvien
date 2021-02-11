//using CoreApp.Authentication.Jwt;
//using CoreApp.EntityFramework.Models;
//using CoreApp.Repository;
//using CoreApp.Service.Bussiness.Implementations;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Collections.Generic;
//using Xunit;

//namespace CoreApp.DomainTest.SystemsTests
//{
//    public class BookPositionTest
//    {
//        private Mock<IRepository<DoRack>> _rep = new Mock<IRepository<DoRack>>();
//        private Mock<IRepository<DoRackService>> _doRackS = new Mock<IRepository<DoRackService>>();
//        private Mock<IRepository<DoBookItem>> _rep1 = new Mock<IRepository<DoBookItem>>();
//        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
//        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

//        public BookPositionTest()
//        {

//        }

//        [Fact]
//        public List<DoRack> GetAlls()
//        {
//            //Arrange
//            var response = new List<DoRack>() {
//                new DoRack
//                {
//                    Number = 43,
//                    LocationIndentifier = "Tủ sách nằm ở vị trí số 43"
//                }
//            };

//            var response1 = new List<DoRack>() {
//                new DoRack
//                {
//                    Number = 49,
//                    LocationIndentifier = "Tủ sách nằm ở vị trí số 49"
//                }
//            };

//            var response2 = new List<DoRack>() {
//                new DoRack
//                {
//                    Number = 1,
//                    LocationIndentifier = "Tủ sách nằm ở vị trí số 1"
//                }
//            };
//            _rep.Setup(x => x.GetAll());
//            _rep1.Setup(x => x.GetAll());
//            _userManager.Setup(x => x.GetAll());
//            _doRackS.Setup(x => x.GetAll());
//            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
//            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

//            return response;
//            return response1;
//            return response2;
//        }

//        //Get By ID.
//        [Fact]
//        public List<DoRack> GetByIds()
//        {
//            //Arrange
//            var getbyid = new List<DoRack>() {
//                new DoRack
//                {
//                    Number =  63,
//                    LocationIndentifier = "Tủ sách nằm ở vị trí số 63"
//                }
//            };
//            _rep.Setup(x => x.GetById());
//            _rep1.Setup(x => x.GetById());
//            _userManager.Setup(x => x.GetById());
//            _doRackS.Setup(x => x.GetById());
//            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
//            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

//            return getbyid;
//        }

//        //Create.
//        [Fact]
//        public List<DoRack> Inserts()
//        {
//            //Arrange
//            var inserts = new List<DoRack>() {
//                new DoRack
//                {
//                    Number = 76,
//                    LocationIndentifier = "Tủ sách nằm ở vị trí số 76",
//                }
//            };
//            _rep.Setup(x => x.SaveChanges());
//            _rep1.Setup(x => x.SaveChanges());
//            _userManager.Setup(x => x.SaveChanges());
//            _doRackS.Setup(x => x.SaveChanges());
//            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
//            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

//            return inserts;

//        }

//        //Update.
//        [Fact]
//        public List<DoRack> Updates()
//        {
//            //Arrange
//            var update = new List<DoRack>() {
//                new DoRack
//                {
//                    Number = 54,
//                    LocationIndentifier = "Tủ sách nằm ở vị trí số 54"
//                }
//            };
//            _rep.Setup(x => x.SaveChanges());
//            _rep1.Setup(x => x.SaveChanges());
//            _userManager.Setup(x => x.SaveChanges());
//            _doRackS.Setup(x => x.SaveChanges());
//            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
//            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

//            return update;

//        }

//        //Delete.
//        [Fact]
//        public List<DoRack> Delete()
//        {
//            //Arrange
//            var del = new List<DoRack>() {
//                new DoRack
//                {
//                    Number = 91,
//                    LocationIndentifier = "Tủ sách nằm ở vị trí số 91"
//                }
//            };
//            _rep.Setup(x => x.GetAll().Clear());
//            _rep1.Setup(x => x.GetAll().Clear());
//            _userManager.Setup(x => x.GetAll());
//            _doRackS.Setup(x => x.GetAll().Clear());
//            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
//            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

//            return del;
//        }

//    }
//}
