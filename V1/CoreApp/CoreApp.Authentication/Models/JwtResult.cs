using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using static CoreApp.Common.Constants.DataConstants;

namespace CoreApp.Authentication.Models
{
    public class JwtResult<T>
    {
        private string _NextAction;
        public T User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string NextAction { get => _NextAction ?? ActionType.Default; set => _NextAction = value; }
    }
}
