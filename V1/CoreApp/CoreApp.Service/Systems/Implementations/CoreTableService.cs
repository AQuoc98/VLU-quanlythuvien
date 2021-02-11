using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Service.Base;
using CoreApp.Service.Systems.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.Authentication.Jwt;
using System.Collections.Generic;
using CoreApp.Common.Models;
using System;
using CoreApp.Common.Enums;

namespace CoreApp.Service.Systems.Implementations
{
    public sealed class CoreTableService : BaseService<CoreTable>, ICoreTableService
    {
        #region Fields
        private readonly ICoreTableDm _dm;
        #endregion

        #region Contructors
        public CoreTableService(ICoreTableDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion

        #region Utilities
        private string[] ValidateBeForSave(CoreTable entity)
        {
            var resultMessages = new List<string>();

            if (string.IsNullOrEmpty(entity.SqlName))
            {
                resultMessages.Add("TABLE_ADD.VALIDATE_SQLNAME_REQUIRE");
            }

            if (string.IsNullOrEmpty(entity.Name))
            {
                resultMessages.Add("TABLE_ADD.TABLE_ADD.VALIDATE_NAME_REQUIRE");
            }

            if (string.IsNullOrEmpty(entity.Alias))
            {
                resultMessages.Add("TABLE_ADD.VALIDATE_ALIAS_REQUIRE");
            }

            if (entity.ModuleId == Guid.Empty)
            {
                resultMessages.Add("TABLE_ADD.VALIDATE_MODULE_REQUIRE");
            }

            if (_dm.CheckAliasExisted(entity))
            {
                resultMessages.Add("TABLE_ADD.VALIDATE_ALIAS_EXISTED");
            }

            return resultMessages.ToArray();
        }
        #endregion

        #region Methods
        public override ResultModel Insert(CoreTable entity)
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

        public override ResultModel Update(CoreTable entity)
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

        public IList<CoreDataType> GetDataType()
        {
            return _dm.GetDataType();
        }
        #endregion
    }
}
