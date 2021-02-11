using CoreApp.Common.Helpers;
using CoreApp.Domain.Base;
using CoreApp.Domain.Systems.Interfaces;
using CoreApp.Repository;
using CoreApp.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CoreApp.Common.Models;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;
using CoreApp.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace CoreApp.Domain.Systems.Implementations
{
    public sealed class CoreViewDm : BaseDomain<CoreView>, ICoreViewDm
    {
        #region Fields
        private readonly IRepository<CoreColumn> _columnRep;
        private readonly IRepository<CoreTable> _tableRep;
        private readonly IRepository<CoreViewColumn> _viewColumnRep;
        private readonly IRepository<CoreViewSearchCondition> _viewSearchConditionRep;
        private readonly IRepository<CoreEnumValue> _enumValueRep;
        private readonly IMapper _mapper;
        #endregion

        #region Contructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="rep"></param>
        /// <param name="columnRep"></param>
        /// <param name="tableRep"></param>
        /// <param name="viewColumnRep"></param>
        /// <param name="dataTypeRep"></param>
        /// <param name="viewSearchConditionRep"></param>
        /// <param name="mapper"></param>
        public CoreViewDm(IRepository<CoreView> rep,
            IRepository<CoreColumn> columnRep,
            IRepository<CoreTable> tableRep,
            IRepository<CoreViewColumn> viewColumnRep,
            IRepository<CoreViewSearchCondition> viewSearchConditionRep,
            IRepository<CoreEnumValue> enumValueRep,
            IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IRepository<CoreUser> userRep) : base(rep, httpContextAccessor, userRep)
        {
            _columnRep = columnRep;
            _tableRep = tableRep;
            _viewColumnRep = viewColumnRep;
            _viewSearchConditionRep = viewSearchConditionRep;
            _enumValueRep = enumValueRep;
            _mapper = mapper;
        }
        #endregion

        #region Utilities

        #endregion

        #region Methods
        /// <summary>
        /// Query Dynamic Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<object> QueryData(QueryDataModel model)
        {
            // 1. Get Columns
            var viewColumns = _viewColumnRep.Table.Where(p => p.ViewId == model.ViewId && p.IsActived).ToList();
            var columnIds = viewColumns.Select(s => s.ColumnId).ToArray();
            var columns = _columnRep.Include(i => i.DataType).Where(p => columnIds.Contains(p.Id)).ToList();
            model.Columns = new List<Common.Models.DataColumn>();
            foreach (var item in columns)
            {
                var viewColumn = viewColumns.Single(s => s.ColumnId == item.Id);
                model.Columns.Add(new Common.Models.DataColumn
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameDict = item.NameDict,
                    EnumId = item.EnumId,
                    DataTypeCode = item.DataType.Code,
                    IsForeignKey = item.IsForeignKey,
                    IsPrimaryKey = item.IsPrimaryKey,
                    Position = viewColumn.Position,
                    Visible = viewColumn.Visible,
                    Searchable = item.Searchable,
                    Sortable = item.Sortable,
                    Width = viewColumn.Width,
                    TableAlias = item.TableAlias,
                    SqlName = StringHelper.ConverToCamelCase(item.SqlName)
                });
            }

            // 2. Create SqlParameters
            var moduleIdParam = new SqlParameter("moduleId", model.ModuleId);
            var viewIdParam = new SqlParameter("viewId", model.ViewId);
            var orderExpressionParam = model.OrderExpression != null ?
                new SqlParameter("orderExpression", model.OrderExpression)
                : new SqlParameter("orderExpression", DBNull.Value);
            var pageIndexParam = new SqlParameter("pageIndex", model.PageIndex);
            var pageSizeParam = new SqlParameter("pageSize", model.PageSize);
            var currentUserIdParam = new SqlParameter("userId", model.UserId);

            var totalRowParam = new SqlParameter
            {
                ParameterName = "total",
                SqlDbType = SqlDbType.Int,
                Value = DBNull.Value,
                Direction = ParameterDirection.Output
            };

            var groupJsonParam = new SqlParameter
            {
                ParameterName = "groupJson",
                SqlDbType = SqlDbType.NVarChar,
                Size = 4000,
                Value = DBNull.Value,
                Direction = ParameterDirection.Output
            };
            var typeInfo = ObjectBuilder.CompileResultTypeInfo("QueryDataResult", "QueryDataResult", model.Columns);

            // 3. Excute query
            var result = _rep.ExcuteStoreProcedure(typeInfo,
                SqlStoreProcedureConstant.SpQueryData,
                moduleIdParam,
                viewIdParam,
                orderExpressionParam,
                pageIndexParam,
                pageSizeParam,
                currentUserIdParam,
                totalRowParam,
                groupJsonParam);

            model.TotalRecord = (int)totalRowParam.Value;
            model.GroupJson = (string)groupJsonParam.Value;
            //model.GroupByObjs = JsonConvert.DeserializeObject<List<GroupByObj>>((string)groupJsonParam.Value);

            return result.ToList();
        }

        /// <summary>
        /// Search Dynamic Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<object> SearchData(SearchDataModel model)
        {
            // 1. Set Conditions
            var conditionColumnIds = model.SearchConditions.Where(p => !string.IsNullOrEmpty(p.Value)).Select(s => s.ColumnId).ToArray();

            // Delete conditions
            var deleteConditions = _viewSearchConditionRep.Table.Where(p => !conditionColumnIds.Contains(p.ColumnId) &&
            p.ViewId == model.QueryDataModel.ViewId &&
            p.CreatedBy == model.QueryDataModel.UserId).ToList();

            _viewSearchConditionRep.Delete(deleteConditions);

            // Update conditions
            var updateConditions = _viewSearchConditionRep.Table.Where(p => conditionColumnIds.Contains(p.ColumnId) &&
           p.ViewId == model.QueryDataModel.ViewId &&
           p.CreatedBy == model.QueryDataModel.UserId).ToList();

            foreach (var item in updateConditions)
            {
                var updateCondition = model.SearchConditions.Single(s => s.ColumnId == item.ColumnId);
                item.Operator = updateCondition.Operator;
                item.Value = updateCondition.Value;
                item.Condition = updateCondition.Condition;
            }

            _viewSearchConditionRep.Update(updateConditions);

            // Add conditions
            var updateConditionColumnIds = updateConditions.Select(s => s.ColumnId).ToArray();
            var conditionsNeedToInsert = model.SearchConditions.Where(p => !updateConditionColumnIds.Contains(p.ColumnId)).ToList();
            var insertConditions = _mapper.Map<List<CoreViewSearchCondition>>(conditionsNeedToInsert);

            _viewSearchConditionRep.Insert(insertConditions);

            // Commit
            SaveChanges();

            // 2. Start Search
            // 2.1 Get Columns
            var viewColumns = _viewColumnRep.Table.Where(p => p.ViewId == model.QueryDataModel.ViewId && p.IsActived).ToList();
            var columnIds = viewColumns.Select(s => s.ColumnId).ToArray();
            var columns = _columnRep.Include(i => i.DataType).Where(p => columnIds.Contains(p.Id)).ToList();
            model.QueryDataModel.Columns = new List<Common.Models.DataColumn>();
            foreach (var item in columns)
            {
                var viewColumn = viewColumns.Single(s => s.ColumnId == item.Id);
                model.QueryDataModel.Columns.Add(new Common.Models.DataColumn
                {
                    Id = item.Id,
                    Name = item.Name,
                    NameDict = item.NameDict,
                    EnumId = item.EnumId,
                    DataTypeCode = item.DataType.Code,
                    IsForeignKey = item.IsForeignKey,
                    IsPrimaryKey = item.IsPrimaryKey,
                    Position = viewColumn.Position,
                    Visible = viewColumn.Visible,
                    Searchable = item.Searchable,
                    Sortable = item.Sortable,
                    Width = viewColumn.Width,
                    TableAlias = item.TableAlias,
                    SqlName = StringHelper.ConverToCamelCase(item.SqlName)
                });
            }

            // 2.2. Create SqlParameters
            var moduleIdParam = new SqlParameter("moduleId", model.QueryDataModel.ModuleId);
            var viewIdParam = new SqlParameter("viewId", model.QueryDataModel.ViewId);
            var orderExpressionParam = model.QueryDataModel.OrderExpression != null ?
                new SqlParameter("orderExpression", model.QueryDataModel.OrderExpression)
                : new SqlParameter("orderExpression", DBNull.Value);
            var pageIndexParam = new SqlParameter("pageIndex", model.QueryDataModel.PageIndex);
            var pageSizeParam = new SqlParameter("pageSize", model.QueryDataModel.PageSize);
            var currentUserIdParam = new SqlParameter("userId", model.QueryDataModel.UserId);

            var totalRowParam = new SqlParameter
            {
                ParameterName = "total",
                SqlDbType = SqlDbType.Int,
                Value = DBNull.Value,
                Direction = ParameterDirection.Output
            };

            var groupJsonParam = new SqlParameter
            {
                ParameterName = "groupJson",
                SqlDbType = SqlDbType.NVarChar,
                Size = 4000,
                Value = DBNull.Value,
                Direction = ParameterDirection.Output
            };
            var typeInfo = ObjectBuilder.CompileResultTypeInfo("QueryDataResult", "QueryDataResult", model.QueryDataModel.Columns);

            // 2.3. Excute query
            var result = _rep.ExcuteStoreProcedure(typeInfo,
                SqlStoreProcedureConstant.SpSearchData,
                moduleIdParam,
                viewIdParam,
                orderExpressionParam,
                pageIndexParam,
                pageSizeParam,
                currentUserIdParam,
                totalRowParam,
                groupJsonParam);

            model.QueryDataModel.TotalRecord = (int)totalRowParam.Value;
            model.QueryDataModel.GroupJson = (string)groupJsonParam.Value;
            //model.GroupByObjs = JsonConvert.DeserializeObject<List<GroupByObj>>((string)groupJsonParam.Value);

            return result.ToList();
        }

        public override CoreView GetById(Guid id)
        {
            var result = _rep.GetById(id);
            result.CoreViewColumns = GetViewDetails(result.Id, result.ModuleId);
            return result;
        }

        public override int Update(CoreView entity)
        {
            var detailAddList = entity.CoreViewColumns.Where(p => p.Id == Guid.Empty && p.IsActived).ToList();
            var detailUpdateList = entity.CoreViewColumns.Where(p => p.Id != Guid.Empty && p.IsActived).ToList();
            var detailUpdateIds = detailUpdateList.Select(s => s.Id).ToArray();
            var detailDeleteList = _viewColumnRep.Table.Where(p => p.ViewId == entity.Id && !detailUpdateIds.Contains(p.Id)).ToList();

            _viewColumnRep.Insert(detailAddList);
            _viewColumnRep.Update(detailUpdateList);
            _viewColumnRep.Delete(detailDeleteList);

            _rep.Update(entity);

            return SaveChanges();
        }

        public IList<CoreViewColumn> GetViewDetails(Guid viewId, Guid moduleId)
        {
            var result = new List<CoreViewColumn>();

            var viewColumns = _viewColumnRep.Table.Where(p => p.ViewId == viewId).ToList();
            var tableByModule = _tableRep.Table.First(f => f.ModuleId == moduleId);
            var columnsByModule = _columnRep.Table.Where(p => p.TableId == tableByModule.Id).ToList();

            foreach (var col in columnsByModule)
            {
                col.Table = null;
                var viewColumn = viewColumns.FirstOrDefault(s => s.ColumnId == col.Id) ?? new CoreViewColumn() { ColumnId = col.Id };
                viewColumn.Column = col;
                result.Add(viewColumn);
            }

            return result;
        }

        public IList<CoreView> GetViewsByModule(Guid moduleId)
        {
            return _rep.Table.Where(p => p.ModuleId == moduleId).ToList();
        }

        public GridViewFilterControl GetGridViewFilterSelectData(Guid moduleId)
        {
            var result = new GridViewFilterControl();

            // Get table by module
            var table = _tableRep.Include(i => i.CoreColumns).First(f => f.ModuleId == moduleId);

            // Get enum
            var columnsHaveEnum = table.CoreColumns.Where(p => p.EnumId != Guid.Empty).ToList();
            var enumIds = columnsHaveEnum.Select(s => s.EnumId).ToArray();
            var enumValues = _enumValueRep.Table.Where(p => enumIds.Contains(p.EnumId)).ToList();
            foreach (var column in columnsHaveEnum)
            {
                var enumValuesByColumn = enumValues.Where(p => p.EnumId == column.EnumId).Select(s => new GridViewSelect
                {
                    Id = s.Id,
                    Name = s.Name,
                    ColumnId = column.Id,
                    ColumnSqlName = column.SqlName
                }).ToList();
                result.AddSelectData(enumValuesByColumn);
            }

            // Get forging values
            var columnBuilder = new List<Common.Models.DataColumn>
            {
                new Common.Models.DataColumn{ SqlName = "Id", DataTypeCode = DataConstants.DataTypes.UniqueIdentifier},
                new Common.Models.DataColumn{ SqlName = "Name", DataTypeCode = DataConstants.DataTypes.Text}
            };
            var typeInfo = ObjectBuilder.CompileResultTypeInfo("ForgingValue", "GridViewFilterControl", columnBuilder);

            var columnsHaveForgingKey = table.CoreColumns.Where(p => p.IsForeignKey == true).ToList();
            foreach (var column in columnsHaveForgingKey)
            {
                var queryResult = _rep.ExcuteSqlQuery(typeInfo, column.SqlScript).ToList();
                var forgingValuesByColumn = queryResult.Select(s => new GridViewSelect
                {
                    Id = ObjectBuilder.GetValueOfObject<Guid>(s, "Id"),
                    Name = ObjectBuilder.GetValueOfObject<string>(s, "Name"),
                    ColumnId = column.Id,
                    ColumnSqlName = column.SqlName
                }).ToList();
                result.AddSelectData(forgingValuesByColumn);
            }

            return result;
        }
        #endregion
    }
}
