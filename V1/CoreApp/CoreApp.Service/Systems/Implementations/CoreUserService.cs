using CoreApp.Authentication.Jwt;
using CoreApp.Common.Constants;
using CoreApp.Common.Enums;
using CoreApp.Common.Helpers;
using CoreApp.Common.Models;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using CoreApp.Service.Base;
using CoreApp.Service.Systems.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace CoreApp.Service.Systems.Implementations
{
    public sealed class CoreUserService : BaseService<CoreUser>, ICoreUserService
    {
        #region Fields
        private readonly ICoreUserDm _dm;
        #endregion

        #region Contructors
        public CoreUserService(ICoreUserDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion

        #region Utilities
        public override string[] ValidateBeForSave(CoreUser entity)
        {
            var resultMessages = new List<string>();

            if (_dm.CheckingForUpdate(entity) != null)
            {
                resultMessages.Add("Số Điện Thoại Đã Tồn Tại");
            }
            else if (_dm.CheckingForEmail(entity) != null)
            {
                resultMessages.Add("Email đã tồn tại");
            }
            else if (_dm.CheckingForName(entity) != null)
            {
                resultMessages.Add("Tên đã Tồn Tại");
            }

            return resultMessages.ToArray();
        }
        public override string[] ValidateBeForPhone(CoreUser entity)
        {
            var resultMessages = new List<string>();
            if (_dm.CheckAccountExisted(entity.CoreCredentials.First()) != null)
            {
                resultMessages.Add("Tài Khoản Đã Tồn Tại");
            }
            else
            if (_dm.CheckPhoneExisted(entity) != null)
            {
                resultMessages.Add("Số Điện Thoại Đã Tồn Tại");
            }
            else if (_dm.CheckingForEmail(entity) != null)
            {
                resultMessages.Add("Email đã tồn tại");
            }
            else if (_dm.CheckingForName(entity) != null)
            {
                resultMessages.Add("Tên đã Tồn Tại");
            }
            return resultMessages.ToArray();
        }

        private string[] ValidateBeForRegister(CoreUser entity)
        {
            var resultMessages = new List<string>();

            if (string.IsNullOrEmpty(entity.Phone))
            {
                resultMessages.Add("Số điện thoại không được để trống.");
            }

            var currentFireBaseIdentifier = StringHelper.ConvertToVnPhone(CurrentFireBaseIdentifier);
            if (currentFireBaseIdentifier != entity.Phone)
            {
                resultMessages.Add("Số điện thoại không khớp với số điện thoại bạn đăng ký.");
            }

            return resultMessages.ToArray();
        }

        private string[] ValidateBeForUpdateInformations(CoreUser entity)
        {
            var resultMessages = new List<string>();

            if (string.IsNullOrEmpty(entity.Name))
            {
                resultMessages.Add("Họ tên không được để trống.");
            }

            return resultMessages.ToArray();
        }
        #endregion

        #region Methods
        public override CoreUser GetById(Guid id)
        {
            var result = base.GetById(id);

            return result;
        }

        public override ResultModel Insert(CoreUser entity)
        {
            var resultModel = new ResultModel();
            resultModel.Messages = ValidateBeForPhone(entity);

            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }
            // Set audit fields
            SetAuditFields(entity);
            foreach (var item in entity.CoreCredentials)
            {
                item.Secret = item.Secret.Length > 0 ? EncryptHelper.Hash(item.Secret) : item.Secret;
            }
            var countSaved = _dm.Insert(entity);
            if (countSaved == 0)
            {
                // Save Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }
            resultModel.Messages = new string[] { "COMMON.MESSAGE_ADD_SUCCESS" };
            resultModel.Status = ResultStatus.Success;
            resultModel.RecordId = entity.Id;

            return resultModel;
        }

        public override ResultModel Update(CoreUser entity)
        {
            var resultModel = new ResultModel();
            resultModel.Messages = ValidateBeForSave(entity);

            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }
            // Set audit fields
            SetAuditFields(entity);
            foreach (var item in entity.CoreCredentials)
            {
                item.Secret = item.Secret.Length > 0 ? EncryptHelper.Hash(item.Secret) : item.Secret;
            }
            var countSaved = _dm.Update(entity);
            if (countSaved == 0)
            {
                // Save Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }
            resultModel.Messages = new string[] { "COMMON.MESSAGE_UPDATE_SUCCESS" };
            resultModel.Status = ResultStatus.Success;
            resultModel.RecordId = entity.Id;

            return resultModel;
        }

        public ResultModel EditProfile(CoreUser entity)
        {
            var resultModel = new ResultModel();
            resultModel.Messages = ValidateBeForSave(entity);

            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }
            // Check Old Password
            if (ValidProfile(entity) == ResultStatus.ValidateFail)
            {
                resultModel.Status = ResultStatus.ValidateFail;
                resultModel.Messages = new string[] { "COMMON.MESSAGE_EDIT_PROFILE_ERROR" };
                return resultModel;
            }

            // Set audit fields
            SetAuditFields(entity);
            foreach (var item in entity.CoreCredentials)
            {
                item.Secret = item.NewSecret.Length > 0 ? EncryptHelper.Hash(item.NewSecret) : "";
                //item.NewSecret = item.cNewSecret.Length > 0 ? EncryptHelper.Hash(item.cNewSecret) : "";
                //if(item.NewSecret != item.cNewSecret)
                //{
                //    resultModel.Status = ResultStatus.ValidateFail;
                //    resultModel.Messages = new string[] { "Chưa xác nhận được mật khẩu mới" };
                //    return resultModel;
                //}
            }

            var countSaved = _dm.Update(entity);

            if (countSaved == 0)
            {
                // Save Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }
            resultModel.Messages = new string[] { "COMMON.MESSAGE_UPDATE_SUCCESS" };
            resultModel.Status = ResultStatus.Success;
            resultModel.RecordId = entity.Id;

            return resultModel;
        }

        private ResultStatus ValidProfile(CoreUser entity)
        {
            var entityCheck = _dm.GetById(entity.Id);
            foreach (var item in entity.CoreCredentials)
            {
                if (item.Secret.Length == 0) return ResultStatus.Success;
                foreach (var itemCheck in entityCheck.CoreCredentials)
                {
                    if (EncryptHelper.ValidateHash(item.Secret, itemCheck.Secret)) return ResultStatus.Success;
                }
            }
            return ResultStatus.ValidateFail;
        }

        public ResultModel Register(CoreUser entity)
        {
            var resultModel = new ResultModel
            {
                Messages = ValidateBeForRegister(entity)
            };

            var checkAccountExisted = _dm.GetUserByIdentifier(entity.Phone);

            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }
            var countSaved = _dm.Insert(entity);
            if (countSaved == 0)
            {
                // Save Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            resultModel.Messages = new string[] { "Account need fill more informations." };
            resultModel.Status = ResultStatus.AccountNeedFillInformations;
            resultModel.RecordId = entity.Id;

            return resultModel;
        }

        public ResultModel UpdateInformations(CoreUser entity)
        {
            var resultModel = new ResultModel
            {
                Messages = ValidateBeForUpdateInformations(entity)
            };

            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }

            var currentUser = _dm.GetById(CurrentUserId);

            currentUser.Name = entity.Name;
            currentUser.Email = entity.Email;
            // Set audit fields
            var countSaved = _dm.Update(currentUser);
            if (countSaved == 0)
            {
                // Save Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            resultModel.Messages = new string[] { "Cập nhật thông tin thành công." };
            resultModel.Status = ResultStatus.Success;
            resultModel.RecordId = currentUser.Id;

            return resultModel;
        }

        public ResultModel UpdateInfoRegister(CoreUser entity)
        {
            var resultModel = new ResultModel
            {
                Messages = ValidateBeForUpdateInformations(entity)
            };

            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }

            var currentUser = CurrentUser;

            currentUser.Name = entity.Name;
            // Set audit fields
            var countSaved = _dm.Update(currentUser);
            if (countSaved == 0)
            {
                // Save Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            resultModel.Messages = new string[] { "Cập nhật thông tin thành công." };
            resultModel.Status = ResultStatus.Success;
            resultModel.RecordId = currentUser.Id;

            return resultModel;
        }

        public ResultModel ChangePassword(ChangePasswordVM entity)
        {
            var resultModel = new ResultModel();

            var countSaved = _dm.ChangePassword(entity);
            if (countSaved == 0)
            {
                // Fail
                resultModel.Messages = new string[] { "Đổi mật khẩu thất bại." };
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }
            if (countSaved == -1)
            {
                // Fail
                resultModel.Messages = new string[] { "Không tìm thấy thông tin đăng nhập. Vui lòng liên hệ Admin" };
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }
            if (countSaved == -2)
            {
                // Fail
                resultModel.Messages = new string[] { "Mật khẩu cũ không khớp." };
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            resultModel.Messages = new string[] { "Đổi mật khẩu thành công." };
            resultModel.Status = ResultStatus.Success;

            return resultModel;
        }

        public ResultModel UpdateUserMobile(CoreUser entity)
        {
            var resultModel = new ResultModel
            {
                Messages = ValidateBeForUpdateInformations(entity)
            };

            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }

            // Set audit fields
            var countSaved = _dm.Update(entity);
            if (countSaved == 0)
            {
                // Save Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            resultModel.Messages = new string[] { "Cập nhật thông tin thành công." };
            resultModel.Status = ResultStatus.Success;
            resultModel.RecordId = entity.Id;

            return resultModel;
        }
        #endregion
    }
}
