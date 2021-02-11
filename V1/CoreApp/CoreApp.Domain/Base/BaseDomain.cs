using CoreApp.Common.Helpers;
using CoreApp.EntityFramework.Models;
using CoreApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreApp.Domain.Base
{
    public class BaseDomain<T> : IBaseDomain<T> where T : class
    {
        #region Fields

        protected readonly IRepository<T> _rep;
        private readonly ClaimsPrincipal _caller;
        protected readonly IRepository<CoreUser> _userRep;
        #endregion

        #region Contructor

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="rep">Repository</param>
        protected BaseDomain(IRepository<T> rep, IHttpContextAccessor httpContextAccessor, IRepository<CoreUser> userRep)
        {
            _rep = rep;
            _caller = httpContextAccessor.HttpContext.User;
            _userRep = userRep;
        }

        #endregion

        #region Utilities

        #endregion

        #region Methods

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>Result</returns>
        public virtual IList<T> GetAll()
        {
            return _rep.GetAll();
        }

        /// <summary>
        /// Get async all
        /// </summary>
        /// <returns>Result</returns>
        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _rep.GetAllAsync();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Object</param>
        /// <returns>Result</returns>
        public virtual T GetById(Guid id)
        {
            return _rep.GetById(id);
        }

        /// <summary>
        /// Get async by Id 
        /// </summary>
        /// <param name="id">Object</param>
        /// <returns>Result</returns>
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _rep.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        public virtual int Insert(T entity)
        {
            _rep.Insert(entity);
            return _rep.SaveChanges();
        }

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        public virtual async Task<int> InsertAsync(T entity)
        {
            await _rep.InsertAsync(entity);
            return await _rep.SaveChangesAsync();
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Result</returns>
        public virtual int Insert(IEnumerable<T> entities)
        {
            _rep.Insert(entities);
            return _rep.SaveChanges();
        }

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Result</returns>
        public virtual async Task<int> InsertAsync(IEnumerable<T> entities)
        {
            await _rep.InsertAsync(entities);
            return await _rep.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual int Update(T entity)
        {
            _rep.Update(entity);
            return _rep.SaveChanges();
        }

        /// <summary>
        /// Update async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<int> UpdateAsync(T entity)
        {
            _rep.Update(entity);
            return await _rep.SaveChangesAsync();
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual int Update(IEnumerable<T> entities)
        {
            _rep.Update(entities);
            return _rep.SaveChanges();
        }

        /// <summary>
        /// Update async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<int> UpdateAsync(IEnumerable<T> entities)
        {
            _rep.Update(entities);
            return await _rep.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="patchDtos"></param>
        /// <returns></returns>
        public virtual int Patch(T entity, JsonPatchDocument<T> patchDto)
        {
            _rep.Patch(entity, patchDto);
            return _rep.SaveChanges();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual int Delete(T entity)
        {
            _rep.Delete(entity);
            return _rep.SaveChanges();
        }

        /// <summary>
        /// Delete async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<int> DeleteAsync(T entity)
        {
            _rep.Delete(entity);
            return await _rep.SaveChangesAsync();
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual int Delete(IEnumerable<T> entities)
        {
            _rep.Delete(entities);
            return _rep.SaveChanges();
        }

        /// <summary>
        /// Delete async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<int> DeleteAsync(IEnumerable<T> entities)
        {
            _rep.Delete(entities);
            return await _rep.SaveChangesAsync();
        }

        /// <summary>
        /// Excute command
        /// </summary>
        /// <param name="sqlCommand">String</param>
        /// <param name="parameters">Params objects</param>
        /// <returns>Result</returns>
        public virtual int ExcuteCommand(string sqlCommand, params object[] parameters)
        {
            return _rep.ExcuteCommand(sqlCommand, parameters);
        }

        /// <summary>
        /// Excute command async
        /// </summary>
        /// <param name="sqlCommand">String</param>
        /// <param name="parameters">Params objects</param>
        /// <returns>Result</returns>
        public virtual async Task<int> ExcuteCommandAsync(string sqlCommand, params object[] parameters)
        {
            return await _rep.ExcuteCommandAsync(sqlCommand, parameters);
        }

        /// <summary>
        /// Excute store procedure
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="name">Store procedure name</param>
        /// <param name="parameters">Store procedure input model</param>
        /// <returns>List<TResult></returns>
        public virtual IList<TResult> ExcuteStoreProcedure<TResult>(string name, params object[] parameters) where TResult : new()
        {
            return _rep.ExcuteStoreProcedure<TResult>(name, parameters);
        }

        /// <summary>
        /// Excute store procedure async
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="name">Store procedure name</param>
        /// <param name="parameters">Store procedure input model</param>
        /// <returns>List<TResult></returns>
        public virtual async Task<IList<TResult>> ExcuteStoreProcedureAsync<TResult>(string name, params object[] parameters) where TResult : new()
        {
            return await _rep.ExcuteStoreProcedureAsync<TResult>(name, parameters);
        }

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
        protected virtual int SaveChanges()
        {
            return _rep.SaveChanges();
        }


        /// <summary>
        /// Save Changes Async
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<int> SaveChangesAsync()
        {
            return await _rep.SaveChangesAsync();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get Current User Id
        /// </summary>
        /// <returns></returns>
        public Guid CurrentUserId
        {
            get {
                var identifier = _caller.Claims.SingleOrDefault(c => c.Type == Common.Constants.JwtConstants.ClaimIdentifiers.Id);
                if (identifier != null)
                {
                    return Guid.Parse(identifier.Value);
                }

                identifier = _caller.Claims.SingleOrDefault(c => c.Type == Common.Constants.JwtConstants.ClaimIdentifiers.phone);
                if (identifier != null)
                {
                    var currentUser = _userRep.Table.FirstOrDefault(f => f.Phone == StringHelper.ConvertToVnPhone(identifier.Value));
                    return currentUser?.Id ?? Guid.Empty;
                }
                return Guid.Empty;
            }
        }

        public CoreUser CurrentUser
        {
            get {
                var currentUserId = CurrentUserId;
                return _userRep.GetById(currentUserId) ?? new CoreUser();
            }
        }
        #endregion
    }
}
