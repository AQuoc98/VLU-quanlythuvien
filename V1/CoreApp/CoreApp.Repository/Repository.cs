using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using CoreApp.Repository.Infrastructure.Extentions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

namespace CoreApp.Repository
{
    public partial class Repository<T> : IRepository<T> where T : class
    {
        #region Fields

        // We do not need dispose this context because DI Container help us do that.
        private readonly EntityFramework.Models.CoreAppDbContext _context;
        private DbSet<T> _entities;
        private readonly IMapper _mapper;

        #endregion

        #region Contructors

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context">Object context</param>
        public Repository(EntityFramework.Models.CoreAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Utilities


        /// <summary>
        /// Get full error
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Error</returns>
        private string GetFullErrorText(Exception ex)
        {
            //var msg = string.Empty;
            //foreach (var validationErrors in exc.EntityValidationErrors)
            //    foreach (var error in validationErrors.ValidationErrors)
            //        msg += $"Property: {error.PropertyName} Error: {error.ErrorMessage}" + Environment.NewLine;
            //return msg;
            return ex.Message;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>Result</returns>
        public virtual IList<T> GetAll()
        {
            return Entities.ToList();
        }

        /// <summary>
        /// Get async all
        /// </summary>
        /// <returns>Result</returns>
        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result</returns>
        public virtual T GetById(params object[] id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// Get async by Id c
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result</returns>
        public virtual async Task<T> GetByIdAsync(params object[] id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        public virtual async Task InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                await Entities.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Result</returns>
        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                Entities.AddRange(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <returns>Result</returns>
        public virtual async Task InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                await Entities.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                _context.Detached(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));
                foreach (var entity in entities)
                {
                    Update(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="patchDtos"></param>
        public virtual void Patch(T entity, JsonPatchDocument<T> patchDtos)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            patchDtos.ApplyTo(entity);
            var dbEntityEntry = _context.Entry(entity);
            _context.Detached(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                {
                    _context.Detached(entity);
                    Delete(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Excute command
        /// </summary>
        /// <param name="sqlCommand">String</param>
        /// <param name="parameters">Params objects</param>
        /// <returns>Result</returns>
        public virtual int ExcuteCommand(string sqlCommand, params object[] parameters)
        {
            try
            {
                return _context.Database.ExecuteSqlCommand(sqlCommand, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Excute command async
        /// </summary>
        /// <param name="sqlCommand">String</param>
        /// <param name="parameters">Params objects</param>
        /// <returns>Result</returns>
        public virtual async Task<int> ExcuteCommandAsync(string sqlCommand, params object[] parameters)
        {
            try
            {
                return await _context.Database.ExecuteSqlCommandAsync(sqlCommand, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(GetFullErrorText(ex), ex);
            }
        }

        /// <summary>
        /// Excute sql query
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IList<object> ExcuteSqlQuery(TypeInfo typeInfo, string sqlQuery, params object[] parameters)
        {
            try
            {
                _context.Database.OpenConnection();
                var cmd = _context.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                List<object> result;
                using (var reader = cmd.ExecuteReader())
                {
                    result = reader.MapToListObj(typeInfo).ToList();
                }
                return result;
            }
            finally
            {
                _context.Database.CloseConnection();
            }
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
            try
            {
                _context.Database.OpenConnection();
                var cmd = _context.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = name;
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                List<TResult> result;
                using (var reader = cmd.ExecuteReader())
                {
                    result = reader.HasRows ? reader.MapToList<TResult>().ToList() : new List<TResult>();
                }
                return result;
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        /// <summary>
        /// Excute stored procedure
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IList<object> ExcuteStoreProcedure(TypeInfo typeInfo, string name, params object[] parameters)
        {
            try
            {
                _context.Database.OpenConnection();
                var cmd = _context.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = name;
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                List<object> result;
                using (var reader = cmd.ExecuteReader())
                {
                    result = reader.MapToListObj(typeInfo).ToList();
                }
                return result;
            }
            finally
            {
                _context.Database.CloseConnection();
            }
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
            try
            {
                _context.Database.OpenConnection();
                var cmd = _context.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = name;
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                List<TResult> result;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    result = reader.MapToList<TResult>().ToList();
                }
                return result;
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        /// <summary>
        /// Include
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(Entities, (current, expression) => current.Include(expression));
        }

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Save Changes Async
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Table as Queryable
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        /// <summary>
        /// Table as no tracking
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Transaction
        /// </summary>
        public IDbContextTransaction Transaction
        {
            get
            {
                return _context.Database.BeginTransaction();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    return _entities = _context.Set<T>();
                return _entities;
            }
        }

        #endregion
    }
}
