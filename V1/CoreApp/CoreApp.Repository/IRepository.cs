using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreApp.Repository
{
    public interface IRepository<T> where T : class
    {

        /// <summary>
        /// Table as Queryable
        /// </summary>
        IQueryable<T> Table
        {
            get;
        }

        /// <summary>
        /// Table as no tracking
        /// </summary>
        IQueryable<T> TableNoTracking
        {
            get;
        }

        /// <summary>
        /// Transaction
        /// </summary>
        IDbContextTransaction Transaction
        {
            get;
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        void Delete(T entity);

        /// <summary>
        /// Excute command
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns>int</returns>
        int ExcuteCommand(string sqlCommand, params object[] parameters);

        /// <summary>
        /// Excute command async
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns>int</returns>
        Task<int> ExcuteCommandAsync(string sqlCommand, params object[] parameters);

        /// <summary>
        /// Excure sql query
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<object> ExcuteSqlQuery(TypeInfo typeInfo, string sqlQuery, params object[] parameters);

        /// <summary>
        /// Excute procedure
        /// </summary>
        /// <typeparam name="TResult">Result object type</typeparam>
        /// <param name="name">Store procedure name</param>
        /// <param name="parameters">Store procedure input model</param>
        /// <returns>TResult</returns>
        IList<TResult> ExcuteStoreProcedure<TResult>(string name, params object[] parameters) where TResult : new();

        /// <summary>
        /// Excute procedure
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<object> ExcuteStoreProcedure(TypeInfo typeInfo, string name, params object[] parameters);

        /// <summary>
        /// Excute procedure async
        /// </summary>
        /// <typeparam name="TResult">Result object type</typeparam>
        /// <param name="name">Store procedure name</param>
        /// <param name="parameters">Store procedure input model</param>
        /// <returns>TResult</returns>
        Task<IList<TResult>> ExcuteStoreProcedureAsync<TResult>(string name, params object[] parameters) where TResult : new();

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>T</returns>
        IList<T> GetAll();

        /// <summary>
        /// Get all async
        /// </summary>
        /// <returns>List<T></returns>
        Task<IList<T>> GetAllAsync();

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T</returns>
        T GetById(params object[] id);

        /// <summary>
        /// Get by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T</returns>
        Task<T> GetByIdAsync(params object[] id);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        void Insert(T entity);

        /// <summary>
        /// Insert entities async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        Task InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// Insert entity async 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Update entities 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Update entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        void Update(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="patchDtos"></param>
        void Patch(T entity, JsonPatchDocument<T> patchDtos);

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Save Change Async
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
