using System;

namespace CoreApp.Common.Models
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenInfo
    {
        public Guid UserId { get; set; }
        public string PrivateKey { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
