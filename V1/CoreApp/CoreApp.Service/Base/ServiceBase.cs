using CoreApp.Authentication.Jwt;
using CoreApp.Common.Constants;
using CoreApp.Common.Enums;
using CoreApp.Common.Helpers;
using CoreApp.Common.Models;
using CoreApp.Domain.Base;
using CoreApp.EntityFramework.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Service.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        #region Fields

        private readonly IBaseDomain<T> _dm;
        private readonly IUserManager _userManager;

        #endregion

        #region Contructor

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="dm"></param>
        protected BaseService(IBaseDomain<T> dm, IUserManager userManager)
        {
            _dm = dm;
            _userManager = userManager;
        }

        #endregion

        #region Utilities
        public virtual string[] ValidateBeForSave(T entity)
        {
            var resultMessages = new List<string>();

            return resultMessages.ToArray();
        }
        public virtual string[] ValidateBeForDelete(T entity)
        {
            var resultMessages = new List<string>();

            return resultMessages.ToArray();
        }
        public virtual string[] ValidateBeForBook(T entity)
        {
            var resultMessages = new List<string>();

            return resultMessages.ToArray();
        }
        public virtual string[] ValidateBeForPhone(T entity)
        {
            var resultMessages = new List<string>();

            return resultMessages.ToArray();
        }
        public virtual string[] ValidateBeForAuthor(T entity)
        {
            var resultMessages = new List<string>();

            return resultMessages.ToArray();
        }
        public virtual string[] ValidateBeForUpdate(T entity)
        {
            var resultMessages = new List<string>();

            return resultMessages.ToArray();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>Result</returns>
        public virtual IList<T> GetAll()
        {
            return _dm.GetAll();
        }

        /// <summary>
        /// Get async all
        /// </summary>
        /// <returns>Result</returns>
        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _dm.GetAllAsync();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Object</param>
        /// <returns>Result</returns>
        public virtual T GetById(Guid id)
        {
            return _dm.GetById(id);
        }

        /// <summary>
        /// Get async by Id 
        /// </summary>
        /// <param name="id">Object</param>
        /// <returns>Result</returns>
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dm.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        public virtual ResultModel Insert(T entity)
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
                // Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            // Success
            resultModel.Messages = new string[] { "COMMON.MESSAGE_ADD_SUCCESS" };
            resultModel.Status = ResultStatus.Success;
            resultModel.RecordId = ObjectBuilder.GetValueOfObject<Guid>(entity, FieldConstants.IdentityField);
            return resultModel;
        }

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        public virtual async Task<ResultModel> InsertAsync(T entity)
        {
            SetAuditFields(entity);
            var countSaved = await _dm.InsertAsync(entity);
            var messages = new string[] { "COMMON.MESSAGE_ADD_SUCCESS" };
            return new ResultModel(ObjectBuilder.GetValueOfObject<Guid>(entity, FieldConstants.IdentityField), ResultStatus.Success, messages);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Result</returns>
        public virtual int Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                SetAuditFields(entity);
            }
            return _dm.Insert(entities);
        }

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Result</returns>
        public virtual async Task<int> InsertAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                SetAuditFields(entity);
            }
            return await _dm.InsertAsync(entities);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual ResultModel Update(T entity)
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
            var countSaved = _dm.Update(entity);
            if (countSaved == 0)
            {
                // Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            // Success
            resultModel.Messages = new string[] { "COMMON.MESSAGE_UPDATE_SUCCESS" };
            resultModel.Status = ResultStatus.Success;
            resultModel.RecordId = ObjectBuilder.GetValueOfObject<Guid>(entity, FieldConstants.IdentityField);
            return resultModel;
        }

        /// <summary>
        /// Update async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<ResultModel> UpdateAsync(T entity)
        {
            SetAuditFields(entity);
            var countSaved = await _dm.UpdateAsync(entity);
            var messages = new string[] { "COMMON.MESSAGE_UPDATE_SUCCESS" };
            return new ResultModel(ObjectBuilder.GetValueOfObject<Guid>(entity, FieldConstants.IdentityField), ResultStatus.Success, messages);
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual ResultModel Update(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                SetAuditFields(entity);
            }
            var countSaved = _dm.Update(entities);
            var messages = new string[] { "COMMON.MESSAGE_UPDATE_SUCCESS" };
            return new ResultModel(Guid.Empty, ResultStatus.Success, messages);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual void Patch(T entity, JsonPatchDocument<T> patchDto)
        {
            _dm.Patch(entity, patchDto);
        }

        /// <summary>
        /// Update async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<int> UpdateAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                SetAuditFields(entity);
            }
            return await _dm.UpdateAsync(entities);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual ResultModel Delete(T entity)
        {
            var resultModel = new ResultModel();
            resultModel.Messages = ValidateBeForDelete(entity);
            if (resultModel.Messages != null && resultModel.Messages.Length > 0)
            {
                // Validate Fail
                resultModel.Status = ResultStatus.ValidateFail;
                return resultModel;
            }
            var countSaved = _dm.Delete(entity);
            if (countSaved == 0)
            {
                // Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            // Success
            resultModel.Messages = new string[] { "COMMON.MESSAGE_DELETE_SUCCESS" };
            resultModel.Status = ResultStatus.Success;
            return resultModel;
        }

        /// <summary>
        /// Delete async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<int> DeleteAsync(T entity)
        {
            return await _dm.DeleteAsync(entity);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual ResultModel Delete(IEnumerable<T> entities)
        {
            var resultModel = new ResultModel();

            var countSaved = _dm.Delete(entities);
            if (countSaved == 0)
            {
                // Fail
                resultModel.Status = ResultStatus.Error;
                return resultModel;
            }

            // Success
            resultModel.Messages = new string[] { "COMMON.MESSAGE_DELETE_SUCCESS" };
            resultModel.Status = ResultStatus.Success;
            return resultModel;
        }

        /// <summary>
        /// Delete async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<int> DeleteAsync(IEnumerable<T> entities)
        {
            return await _dm.DeleteAsync(entities);
        }

        /// <summary>
        /// Set value for audit fields
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void SetAuditFields(object entity)
        {
            try
            {
                var currentUserId = CurrentUserId;

                var identityField = ObjectBuilder.GetValueOfObject<Guid>(entity, FieldConstants.IdentityField);
                if (identityField == Guid.Empty)
                {
                    ObjectBuilder.SetValueForObject(entity, currentUserId, FieldConstants.AuditFields.CreatedBy);
                    ObjectBuilder.SetValueForObject(entity, DateTime.Now, FieldConstants.AuditFields.CreatedDate);
                }
                ObjectBuilder.SetValueForObject(entity, currentUserId, FieldConstants.AuditFields.UpdatedBy);
                ObjectBuilder.SetValueForObject(entity, DateTime.Now, FieldConstants.AuditFields.UpdatedDate);
            }
            catch (Exception)
            {
                // Anonymous user
            }

        }
        #endregion

        #region Properties

        /// <summary>
        /// Current user id
        /// </summary>
        public virtual Guid CurrentUserId
        {
            get
            {
                return _userManager.GetCurrentUserId();
            }
        }

        /// <summary>
        /// Current user data
        /// </summary>
        public virtual CoreUser CurrentUser
        {
            get
            {
                return _userManager.GetCurrentUser();
            }
        }

        public virtual string CurrentFireBaseIdentifier
        {
            get
            {
                return _userManager.GetCurrentFireBaseIdentifier();
            }
        }

        #endregion
    }
}
