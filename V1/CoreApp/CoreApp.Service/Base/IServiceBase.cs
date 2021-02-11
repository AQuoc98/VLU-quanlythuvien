using CoreApp.Common.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Service.Base
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        ResultModel Delete(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        ResultModel Delete(T entity);

        /// <summary>
        /// Delete entities async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        Task<int> DeleteAsync(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        Task<int> DeleteAsync(T entity);

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
        T GetById(Guid id);

        /// <summary>
        /// Get by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        int Insert(IEnumerable<T> entities);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        ResultModel Insert(T entity);

        /// <summary>
        /// Insert entities async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        Task<int> InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// Insert entity async 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        Task<ResultModel> InsertAsync(T entity);

        /// <summary>
        /// Update entities 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        ResultModel Update(IList<T> entities);

        /// <summary>
        /// Update entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        ResultModel Update(T entity);

        /// <summary>
        /// Update entities async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>int</returns>
        Task<int> UpdateAsync(IEnumerable<T> entities);

        /// <summary>
        /// Update entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        Task<ResultModel> UpdateAsync(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="patchDtos"></param>
        void Patch(T entity, JsonPatchDocument<T> patchDto);
    }
}
