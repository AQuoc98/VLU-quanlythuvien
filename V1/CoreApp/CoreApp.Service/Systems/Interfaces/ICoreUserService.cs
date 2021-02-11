using CoreApp.Common.Models;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using CoreApp.Service.Base;

namespace CoreApp.Service.Systems.Interfaces
{
    public interface ICoreUserService : IBaseService<CoreUser>
    {
        ResultModel EditProfile(CoreUser entity);
        ResultModel Register(CoreUser entity);
        ResultModel ChangePassword(ChangePasswordVM entity);
        ResultModel UpdateInformations(CoreUser entity);
        ResultModel UpdateInfoRegister(CoreUser entity);
        ResultModel UpdateUserMobile(CoreUser entity);
    }
}
