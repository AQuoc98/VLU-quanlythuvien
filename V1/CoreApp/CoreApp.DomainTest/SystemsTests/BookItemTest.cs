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
    public class BookItemTest
    {
        private Mock<IRepository<DoBookItem>> _rep = new Mock<IRepository<DoBookItem>>();
        private Mock<IRepository<DoBookItemService>> _DoBookItemS = new Mock<IRepository<DoBookItemService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public BookItemTest()
        {

        }

        [Fact]
        public List<DoBookItem> GetAlls()
        {
            System.Guid aa29153a82c545d6a37c863c85f6264c = default;
            System.Guid a4d176c27d4f978a0786d31b6af66251 = default;
            System.Guid e6280a6c4a084b60838a93cfab4a8825 = default;
            System.Guid b8683845e6a24c34b0368d4e79a1043c = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;
            System.Guid b251f97420b644edb50e113b8e59e41c = default;
            System.Guid b6d5018b89fe0c9477f689d40f183474 = default;
            System.Guid bd7a32c4c58f37075c28cb672ea10f76 = default;
            System.Guid cfe6ad83b95afdf5f5482984cbabd8cf = default;
            System.Guid da2d061f5b92a84fa9fd494d3d9f8d1f = default;
            System.Guid e72a58b3901b945cd3fa27d17961529b = default;
            System.Guid b7c7a34674964fd7dba945426a781982 = default;
            System.Guid eb4db68eb2faa54f8b07918de83c9d7e = default;
            System.Guid ad85c8c56747e25b3ce05f7e2bf3beb9 = default;
            System.Guid c628fe6df428d8748e15b75f802a9610 = default;

            //Arrange
            var response = new List<DoBookItem>() {
                new DoBookItem
                {

                    Barcode = "98569795432956",
                    IsReferenceOnly = false,
                    IsRareBook = true,
                    Price = 3200000,
                    PublicationYear = "2020",
                    RackId = a84eccac7d2c4a9db9aa5674f791811c,
                    FormatId = b251f97420b644edb50e113b8e59e41c,
                    BookId = cfe6ad83b95afdf5f5482984cbabd8cf,
                    StatusId = e72a58b3901b945cd3fa27d17961529b,
                    BookLendingId = da2d061f5b92a84fa9fd494d3d9f8d1f
                    
                }
            };

            var response1 = new List<DoBookItem>() {
                new DoBookItem
                {
                    Barcode = "98569795432956",
                    IsReferenceOnly = false,
                    IsRareBook = true,
                    Price = 3200000,
                    PublicationYear = "1998",
                    RackId = aa29153a82c545d6a37c863c85f6264c,
                    FormatId = a4d176c27d4f978a0786d31b6af66251,
                    BookId = b8683845e6a24c34b0368d4e79a1043c,
                    StatusId = b6d5018b89fe0c9477f689d40f183474,
                    BookLendingId = bd7a32c4c58f37075c28cb672ea10f76
                }
            };

            var response2 = new List<DoBookItem>() {
                new DoBookItem
                {
                    Barcode = "98569795432956",
                    IsReferenceOnly = false,
                    IsRareBook = true,
                    Price = 3200000,
                    PublicationYear = "2012",
                    RackId = b7c7a34674964fd7dba945426a781982,
                    FormatId = eb4db68eb2faa54f8b07918de83c9d7e,
                    BookId = ad85c8c56747e25b3ce05f7e2bf3beb9,
                    StatusId = c628fe6df428d8748e15b75f802a9610,
                    BookLendingId = e6280a6c4a084b60838a93cfab4a8825
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _DoBookItemS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By Id.
        [Fact]
        public List<DoBookItem> GetByIds()
        {
            System.Guid eeaeaa170de2b482786f30814e4f4afe4 = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid ed5b3ec1212a4e0f8dbe02ae31eb30e6 = default;
            System.Guid a593ea3b0195a8d5c2829ec312428f42 = default;
            System.Guid a7af3d14c47613ec37ef071075e71212 = default;

            //Arrange
            var getbyid = new List<DoBookItem>() {
                new DoBookItem
                {
                    Barcode = "98569795432956",
                    IsReferenceOnly = false,
                    IsRareBook = true,
                    Price = 3200000,
                    PublicationYear = "2007",
                    RackId = eeaeaa170de2b482786f30814e4f4afe4,
                    FormatId = b85a3624df0948cd99d5e2fe7411874d,
                    BookId = ed5b3ec1212a4e0f8dbe02ae31eb30e6,
                    StatusId = a593ea3b0195a8d5c2829ec312428f42,
                    BookLendingId = a7af3d14c47613ec37ef071075e71212
        }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _DoBookItemS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoBookItem> Inserts()
        {
            System.Guid b522fff3d18442788ea1e7156154940e = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;
            System.Guid e6d6a1c0cea0fa7f75a39edf4baece4 = default;
            System.Guid de0b679e2c492ea59c0cba21fd8a2d67 = default;
            
            //Arrange
            var inserts = new List<DoBookItem>() {
                new DoBookItem
                {
                    Barcode = "98569795432956",
                    IsReferenceOnly = false,
                    IsRareBook = true,
                    Price = 3200000,
                    PublicationYear = "2009",
                    RackId = b522fff3d18442788ea1e7156154940e,
                    FormatId = b85a3624df0948cd99d5e2fe7411874d,
                    BookId = a84eccac7d2c4a9db9aa5674f791811c,
                    StatusId = e6d6a1c0cea0fa7f75a39edf4baece4,
                    BookLendingId = de0b679e2c492ea59c0cba21fd8a2d67
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _DoBookItemS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoBookItem> Updates()
        {
            System.Guid c7ca4ce1f4781a43fc375e9052a635eb = default;
            System.Guid f2d352fc4f11d8ab7657820a029ce246 = default;
            System.Guid c21c6fe7cb1debf4176d252908134988 = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;

            //Arrange
            var update = new List<DoBookItem>() {
                new DoBookItem
                {
                    Barcode = "98569795432956",
                    IsReferenceOnly = false,
                    IsRareBook = true,
                    Price = 3200000,
                    PublicationYear = "2001",
                    RackId = c7ca4ce1f4781a43fc375e9052a635eb,
                    FormatId = f2d352fc4f11d8ab7657820a029ce246,
                    BookId = c21c6fe7cb1debf4176d252908134988,
                    StatusId = b85a3624df0948cd99d5e2fe7411874d,
                    BookLendingId = a84eccac7d2c4a9db9aa5674f791811c
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _DoBookItemS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoBookItem> Delete()
        {
            System.Guid c8a93ef4dccc46f698095472e157ef08 = default;
            System.Guid b85a3624df0948cd99d5e2fe7411874d = default;
            System.Guid a84eccac7d2c4a9db9aa5674f791811c = default;
            System.Guid f0c1c668c6fc4a11adabbaaee48863c2 = default; 
            System.Guid ee0a5fd962707493097af79689468291 = default;

            //Arrange
            var del = new List<DoBookItem>() {
                new DoBookItem
                {
                    Barcode = "98569795432956",
                    IsReferenceOnly = false,
                    IsRareBook = true,
                    Price = 3200000,
                    PublicationYear = "2020",
                    RackId = c8a93ef4dccc46f698095472e157ef08,
                    FormatId = b85a3624df0948cd99d5e2fe7411874d,
                    BookId = a84eccac7d2c4a9db9aa5674f791811c,
                    StatusId = f0c1c668c6fc4a11adabbaaee48863c2,
                    BookLendingId = ee0a5fd962707493097af79689468291
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _DoBookItemS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
