using CoreApp.Common.Models;
using CoreApp.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]/[action]/{id?}")]
    [Authorize]
    public partial class ModuleApiControllerBase<T> : ControllerBase where T : class
    {
        #region Fields

        protected readonly ILogger<ModuleApiControllerBase<T>> _logger;
        private readonly IBaseService<T> _service;
        #endregion

        #region Contructors

        public ModuleApiControllerBase(ILogger<ModuleApiControllerBase<T>> logger, IBaseService<T> service)
        {
            _logger = logger;
            _service = service;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet]
        public virtual IList<T> GetAll()
        {
            return _service.GetAll();
        }

        /// <summary>
        /// Get async all
        /// </summary>
        /// <returns>Result</returns>
        [HttpGet]
        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Object</param>
        /// <returns>Result</returns>
        [HttpGet]
        public virtual T GetById(Guid id)
        {
            return _service.GetById(id);
        }

        /// <summary>
        /// Get async by Id 
        /// </summary>
        /// <param name="id">Object</param>
        /// <returns>Result</returns>
        [HttpGet]
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        [HttpPost]
        public virtual ResultModel Insert( T entity)
        {
            return _service.Insert(entity);
        }

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        [HttpPost]
        public virtual async Task<ResultModel> InsertAsync(T entity)
        {
            return await _service.InsertAsync(entity);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Result</returns>
        [HttpPost]
        public virtual int Inserts(IEnumerable<T> entities)
        {
            return _service.Insert(entities);
        }

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Result</returns>
        [HttpPost]
        public virtual async Task<int> InsertsAsync(IEnumerable<T> entities)
        {
            return await _service.InsertAsync(entities);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        [HttpPost]
        public virtual ResultModel Update(T entity)
        {
            return _service.Update(entity);
        }

        /// <summary>
        /// Update async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        [HttpPut]
        public virtual async Task<ResultModel> UpdateAsync(T entity)
        {
            return await _service.UpdateAsync(entity);
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        [HttpPost]
        public virtual ResultModel Updates(IEnumerable<T> entities)
        {
            return _service.Update(entities.ToList());
        }

        /// <summary>
        /// Update async entities
        /// </summary>
        /// <param name="entities">Entities</param>
        [HttpPut]
        public virtual async Task<int> UpdatesAsync(IEnumerable<T> entities)
        {
            return await _service.UpdateAsync(entities);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entity</param>
        [HttpPost]
        public virtual ResultModel Delete(IEnumerable<T> entities)
        {
            return _service.Delete(entities);
        }
       

        /// <summary>
        /// Delete async entities
        /// </summary>
        /// <param name="entities">Entity</param>
        [HttpPost]
        public virtual async Task<int> DeleteAsync(IEnumerable<T> entities)
        {
            return await _service.DeleteAsync(entities);
        }


        #endregion

        #region Properties

        #endregion
    }
}
