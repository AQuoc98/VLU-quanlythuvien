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
    public class BookTest
    {
        private Mock<IRepository<DoBook>> _rep = new Mock<IRepository<DoBook>>();
        private Mock<IRepository<DoBookService>> _DoBookS = new Mock<IRepository<DoBookService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public BookTest()
        {

        }

        [Fact]
        public List<DoBook> GetAlls()
        {
            System.Guid aa29153a82c545d6a37c863c85f6264c = default;
            System.Guid b23ef6374ae9ab75ad9c428b78617e20 = default;
            System.Guid a4d176c27d4f978a0786d31b6af66251 = default;
            System.Guid e6280a6c4a084b60838a93cfab4a8825 = default;
            System.Guid a98e83c0cfed4387ad1186ba3f7c78ee = default;
            System.Guid b8683845e6a24c34b0368d4e79a1043c = default;
            System.Guid c780256ff5c1452d84b66805d7cc8e89 = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;
            System.Guid b251f97420b644edb50e113b8e59e41c = default;
            //Arrange
            var response = new List<DoBook>() {
                new DoBook
                {
                    ISBN = "4324234254345345",
                    Title = "Những kẻ mộng mơ",
                    Subject = "Mộng mơ",
                    LanguageId = b23ef6374ae9ab75ad9c428b78617e20,
                    NumberOfPages = 100,
                    Image = "9b368c27-d693-4337-bfab-c1cb60218ff8-dautay.jpg",
                    CatalogId = aa29153a82c545d6a37c863c85f6264c,
                    PublishierId = a4d176c27d4f978a0786d31b6af66251,
                    AuthorId = b8683845e6a24c34b0368d4e79a1043c
                }
            };

            var response1 = new List<DoBook>() {
                new DoBook
                {
                    ISBN = "9856979570956",
                    Title = "Ngày tháng ấy",
                    Subject = "Kỉ niệm",
                    LanguageId = b85a3624df0948cd99d5e2fe7411874d,
                    NumberOfPages = 200,
                    Image = "9b368c27-d693-4337-bfab-c1cb60218ff8-ngaythangay.jpg",
                    CatalogId = c780256ff5c1452d84b66805d7cc8e89,
                    PublishierId = e6280a6c4a084b60838a93cfab4a8825,
                    AuthorId = a98e83c0cfed4387ad1186ba3f7c78ee
                }
            };

            var response2 = new List<DoBook>() {
                new DoBook
                {
                   ISBN = "5436769098754",
                    Title = "Tôi thấy hoa vàng trên cỏ xanh",
                    Subject = "Cuộc đời",
                    LanguageId = b251f97420b644edb50e113b8e59e41c,
                    NumberOfPages = 213,
                    Image = "9b368c27-d693-4337-bfab-c1cb60218ff8-asbc.jpg",
                    CatalogId = aa29153a82c545d6a37c863c85f6264c,
                    PublishierId = e6280a6c4a084b60838a93cfab4a8825,
                    AuthorId = a84eccac7d2c4a9db9aa5674f791811c
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _DoBookS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By Id.
        [Fact]
        public List<DoBook> GetByIds()
        {
            System.Guid eeaeaa170de2b482786f30814e4f4afe4 = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;
            System.Guid ed5b3ec1212a4e0f8dbe02ae31eb30e6 = default;
            //Arrange
            var getbyid = new List<DoBook>() {
                new DoBook
                {
                    ISBN = "54353465765786",
                    Title = "Ngày đó không còn xa",
                    Subject = "Viễn tưởng",
                    LanguageId = ed5b3ec1212a4e0f8dbe02ae31eb30e6,
                    NumberOfPages = 213,
                    Image = "9b368c27-d693-4337-bfab-c1cb60218ff8-ngayay.jpg",
                    CatalogId = eeaeaa170de2b482786f30814e4f4afe4,
                    PublishierId = b85a3624df0948cd99d5e2fe7411874d,
                    AuthorId = a84eccac7d2c4a9db9aa5674f791811c
        }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _DoBookS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoBook> Inserts()
        {
            System.Guid b522fff3d18442788ea1e7156154940e = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;
            System.Guid d336cd933f464aca8ff27196a727c5d6 = default;
            //Arrange
            var inserts = new List<DoBook>() {
                new DoBook
                {
                    ISBN = "432425345476575",
                    Title = "Tôi thấy hoa vàng trên cỏ xanh",
                    Subject = "Cuộc đời",
                    LanguageId = d336cd933f464aca8ff27196a727c5d6,
                    NumberOfPages = 213,
                    Image = "9b368c27-d693-4337-bfab-c1cb60218ff8-hoavang.jpg",
                    CatalogId = b522fff3d18442788ea1e7156154940e,
                    PublishierId = b85a3624df0948cd99d5e2fe7411874d,
                    AuthorId = a84eccac7d2c4a9db9aa5674f791811c
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _DoBookS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoBook> Updates()
        {
            System.Guid f5b622f818f640318d580981ed540052 = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;
            System.Guid c8a93ef4dccc46f698095472e157ef08 = default;
            //Arrange
            var update = new List<DoBook>() {
                new DoBook
                {
                    ISBN = "4324234254345345",
                    Title = "Chiếc lược ngà",
                    Subject = "Chiếc lược ngà",
                    LanguageId = c8a93ef4dccc46f698095472e157ef08,
                    NumberOfPages = 213,
                    Image = "9b368c27-d693-4337-bfab-c1cb60218ff8-chiecluocnga.jpg",
                    CatalogId = f5b622f818f640318d580981ed540052,
                    PublishierId = b85a3624df0948cd99d5e2fe7411874d,
                    AuthorId = a84eccac7d2c4a9db9aa5674f791811c
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _DoBookS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoBook> Delete()
        {
            System.Guid c9c5ce24939b1f46010e20d546856356 = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;
            System.Guid f0c1c668c6fc4a11adabbaaee48863c2 = default;
            //Arrange
            var del = new List<DoBook>() {
                new DoBook
                {
                    ISBN = "4324234254345345",
                    Title = "Khu vườn trên mây",
                    Subject = "Cuộc đời",
                    LanguageId = f0c1c668c6fc4a11adabbaaee48863c2,
                    NumberOfPages = 213,
                    Image = "9b368c27-d693-4337-bfab-c1cb60218ff8-khuvuon.jpg",
                    CatalogId = c9c5ce24939b1f46010e20d546856356,
                    PublishierId = b85a3624df0948cd99d5e2fe7411874d,
                    AuthorId = a84eccac7d2c4a9db9aa5674f791811c
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _DoBookS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
