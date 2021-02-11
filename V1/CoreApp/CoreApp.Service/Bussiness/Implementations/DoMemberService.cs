using CoreApp.Common.Constants;
using CoreApp.EntityFramework.Extension;
using CoreApp.Common.Enums;
using CoreApp.Common.Helpers;
using CoreApp.Common.Models;
using CoreApp.Domain.Bussiness.Interfaces;
using CoreApp.EntityFramework.Models;
using CoreApp.EntityFramework.ViewModels;
using CoreApp.Service.Base;
using CoreApp.Service.Bussiness.Interfaces;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using CoreApp.Authentication.Jwt;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.Service.Bussiness.Implementations
{
    public sealed class DoMemberService : BaseService<DoMember>, IMemberService
    {
        #region Fields
        private readonly IMemberDm _dm;

        #endregion

        #region Contructors
        public DoMemberService(IMemberDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;

        }
        #endregion
        #region
        public override string[] ValidateBeForSave(DoMember entity)
        {
            var resultMessages = new List<string>();
            if (_dm.CheckingForExistMemberCode(entity) != null)
            {
                resultMessages.Add("Thành viên đã tồn tại");
            }

            return resultMessages.ToArray();
        }
        public override string[] ValidateBeForUpdate(DoMember entity)
        {
            var resultMessages = new List<string>();
            if (_dm.CheckingUpdateExistMemberCode(entity) != null)
            {
                resultMessages.Add("Thành viên đã tồn tại");
            }

            return resultMessages.ToArray();
        }


        #endregion
        public override ResultModel Insert(DoMember entity)

        {
            var resultModel = new ResultModel();
            resultModel.Messages = ValidateBeForSave(entity);
            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }
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
        public override ResultModel Update(DoMember entity)
        {
            var resultModel = new ResultModel();
            resultModel.Messages = ValidateBeForUpdate(entity);
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

    }
}
