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
    public class BookReaderTest
    {
        private Mock<IRepository<DoMember>> _rep = new Mock<IRepository<DoMember>>();
        private Mock<IRepository<DoMemberService>> _DoMemberS = new Mock<IRepository<DoMemberService>>();
        private Mock<IRepository<UserManager>> _userManager = new Mock<IRepository<UserManager>>();
        private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();

        public BookReaderTest()
        {

        }

        [Fact]
        public List<DoMember> GetAlls()
        {
            System.Guid ef6565524aa2903699e3cf254dd9882e = default;
            System.Guid cad335927bfd4bc3bfe48a0e0804d498 = default;
            System.Guid b19ffd4a64be796fede38acde225c676 = default;
            //Arrange
            var response = new List<DoMember>() {
                new DoMember
                {
                    Name = "Nguyễn Quốc Anh",
                    Address = "76 Nguyễn Văn Lượng",
                    Phone = "0943425434",
                    MemberCode = "6754854321546", 
                    Image = "Nguyen-Quoc-Anh.jpg",
                    Gender = true,
                    MemberGroupId = ef6565524aa2903699e3cf254dd9882e  
                 }
            };

            var response1 = new List<DoMember>() {
                new DoMember
                {
                    Name = "Nguyễn Văn An",
                    Address = "98 Nguyễn Văn Trỗi",
                    Phone = "09534588435",
                    MemberCode = "34573465895483",
                    Image = "Nguyen-Van-An.jpg",
                    Gender = true,
                    MemberGroupId = cad335927bfd4bc3bfe48a0e0804d498
                }
            };

            var response2 = new List<DoMember>() {
                new DoMember
                {
                    Name = "Lê Thị Nguyên",
                    Address = "541 Giải Phóng",
                    Phone = "09675486754",
                    MemberCode = "6547595375635435",
                    Image = "LeThiNguyen.jpg",
                    Gender = false,
                    MemberGroupId = b19ffd4a64be796fede38acde225c676
                }
            };
            _rep.Setup(x => x.GetAll());
            _userManager.Setup(x => x.GetAll());
            _DoMemberS.Setup(x => x.GetAll());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return response;
            return response1;
            return response2;
        }

        //Get By ID.
        [Fact]
        public List<DoMember> GetByIds()
        {
            System.Guid b81e940bc8e84387a8efd1c81fbe24d6 = default;
            //Arrange
            var getbyid = new List<DoMember>() {
                new DoMember
                {
                    Name = "Phạm Văn Nguyên",
                    Address = "904 Lê Lợi",
                    Phone = "0987787874",
                    MemberCode = "5634765357647534",
                    Image = "PhamVanNguyen.jpg",
                    Gender = true,
                    MemberGroupId = b81e940bc8e84387a8efd1c81fbe24d6
                }
            };
            _rep.Setup(x => x.GetById());
            _userManager.Setup(x => x.GetById());
            _DoMemberS.Setup(x => x.GetById());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return getbyid;
        }

        //Create.
        [Fact]
        public List<DoMember> Inserts()
        {
            System.Guid cb00741459278b50065d7889c6535309 = default;
            //Arrange
            var inserts = new List<DoMember>() {
                new DoMember
                {
                    Name = "Trần Cát Minh",
                    Address = "438 Cộng Hòa",
                    Phone = "0954363654",
                    MemberCode = "8975653465765",
                    Image = "TranCatMinh.jpg",
                    Gender = false,
                    MemberGroupId = cb00741459278b50065d7889c6535309
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _DoMemberS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return inserts;

        }

        //Update.
        [Fact]
        public List<DoMember> Updates()
        {
            System.Guid cfc18b4b46e083d553e31b2171ed3134 = default;
            //Arrange
            var update = new List<DoMember>() {
                new DoMember
                {
                    Name = "Lê Văn Sung",
                    Address = "65 Hoàng Văn Thụ",
                    Phone = "09765467675",
                    MemberCode = "45835358935735",
                    Image = "LeVanSung.jpg",
                    Gender = true,
                    MemberGroupId = cfc18b4b46e083d553e31b2171ed3134
                }
            };
            _rep.Setup(x => x.SaveChanges());
            _userManager.Setup(x => x.SaveChanges());
            _DoMemberS.Setup(x => x.SaveChanges());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return update;

        }

        //Delete.
        [Fact]
        public List<DoMember> Delete()
        {
            System.Guid b04b3e4900170d499451f177d8e54843 = default;
            //Arrange
            var del = new List<DoMember>() {
                new DoMember
                {
                    Name = "Phạm Thị Hoa",
                    Address = "62 Bình Giã",
                    Phone = "0935435322",
                    MemberCode = "548859755794237",
                    Image = "PhamThiHoa.jpg",
                    Gender = false,
                    MemberGroupId = b04b3e4900170d499451f177d8e54843
                }
            };
            _rep.Setup(x => x.GetAll().Clear());
            _userManager.Setup(x => x.GetAll());
            _DoMemberS.Setup(x => x.GetAll().Clear());
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() } };
            _httpContextAccessor.Setup(x => x.HttpContext).Returns(ctx.HttpContext);

            return del;
        }

    }
}
