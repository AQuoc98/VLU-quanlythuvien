﻿using CoreApp.Common.Constants;
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

namespace CoreApp.Service.Bussiness.Implementations
{
    public sealed class DoCatalogService : BaseService<DoCatalog>, ICatalogService
    {
        #region Fields
        private readonly ICatalogDm _dm;
        #endregion

        #region Contructors
        public DoCatalogService(ICatalogDm dm, IUserManager userManager) : base(dm, userManager)
        {
            _dm = dm;
        }
        #endregion
        #region validate
        public override string[] ValidateBeForSave(DoCatalog entity)
        {
            var resultMessages = new List<string>();
            if (_dm.CheckingForName(entity) != null)
            {
                resultMessages.Add("Mục lục đã tồn tại");
            }
            return resultMessages.ToArray();
        }
        public override string[] ValidateBeForUpdate(DoCatalog entity)
        {
            var resultMessages = new List<string>();
            if (_dm.CheckingForUpdate(entity) != null)
            {
                resultMessages.Add("Mục lục đã tồn tại");
            }
            return resultMessages.ToArray();
        }
        #endregion
        #region
        public override ResultModel Insert(DoCatalog entity)
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
        public override ResultModel Update(DoCatalog entity)
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
        #endregion
    }


}
