using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize]
    public partial class NormalApiControllerBase : ControllerBase
    {
    }
}
