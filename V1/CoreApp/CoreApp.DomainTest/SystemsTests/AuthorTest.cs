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
    public class AuthorTest
    {
        private Mock<IRepository<DoAuthor>> _rep = new Mock<IRepository<DoAuthor>>();
        private Mock<IRepository<DoAuthorService>> _doAuthorS = new Mock<IRepository<DoAuthorService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public AuthorTest()
        {

        }

        [Fact]
        public List<DoAuthor> GetAlls()
        {
            //Arrange
            var response = new List<DoAuthor>() {
                new DoAuthor
                {
                    Name = "Nguyễn Quang Sáng",
                    Description = "Nguyễn Quang Sáng là nhà văn Việt Nam, từng đoạt Giải thưởng Hồ Chí Minh về Văn học – Nghệ thuật đợt II năm 2000."
                }
            };

            var response1 = new List<DoAuthor>() {
                new DoAuthor
                {
                    Name = "Tố Hữu",
                    Description = "Là một nhà thơ tiêu biểu của thơ cách mạng Việt Nam, đồng thời ông còn là một chính khách, một cán bộ cách mạng lão thành."
                }
            };

            var response2 = new List<DoAuthor>() {
                new DoAuthor
                {
                    Name = "Kim Lân",
                    Description = "Là một nhà văn Việt Nam. Ông được biết đến với các tác phẩm văn học như Vợ nhặt, Làng."
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _doAuthorS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By ID.
        [Fact]
        public List<DoAuthor> GetByIds()
        {
            //Arrange
            var getbyid = new List<DoAuthor>() {
                new DoAuthor
                {
                    Name =  "Xuân Diệu",
                    Description = "Ông nổi tiếng từ phong trào Thơ mới với tập Thơ thơ và Gửi hương cho gió."
                }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _doAuthorS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoAuthor> Inserts()
        {
            //Arrange
            var inserts = new List<DoAuthor>() {
                new DoAuthor
                {
                    Name = "Sach thien van hoc",
                    Description = "D Bà nổi tiếng với nhiều bài thơ được nhiều người biết đến như Thuyền và biển, Sóng, Thơ tình cuối mùa thu, Tiếng gà trưa, ..."
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doAuthorS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoAuthor> Updates()
        {
            //Arrange
            var update = new List<DoAuthor>() {
                new DoAuthor
                {
                    Name = "Lưu Quang Vũ",
                    Description = "Lưu Quang Vũ là nhà soạn kịch, nhà thơ và nhà văn hiện đại của Việt Nam."
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _doAuthorS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoAuthor> Delete()
        {
            //Arrange
            var del = new List<DoAuthor>() {
                new DoAuthor
                {
                    Name = "Quang Dũng",
                    Description = "Ông là tác giả của một số bài thơ nổi tiếng như Tây Tiến, Đôi mắt người Sơn Tây, Đôi bờ... Ngoài ra Quang Dũng còn là một họa sĩ, nhạc sĩ. "
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _doAuthorS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
