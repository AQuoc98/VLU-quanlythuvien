using CoreApp.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace CoreApp.Repository.Infrastructure.Extentions
{
    public static class DataReaderExtentions
    {
        /// <summary>
        /// Map to list
        /// </summary>
        /// <typeparam name="TResult">Object</typeparam>
        /// <param name="dr">Data Reader</param>
        /// <returns>Result</returns>
        public static IList<TResult> MapToList<TResult>(this DbDataReader dr) where TResult : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(TResult);
                var entities = new List<TResult>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (dr.Read())
                {
                    TResult newObject = new TResult();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        if (propDict.ContainsKey(dr.GetName(i).ToUpper()))
                        {
                            var info = propDict[dr.GetName(i).ToUpper()];
                            if (info != null && info.CanWrite)
                            {
                                var val = dr.GetValue(i);
                                info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }

        /// <summary>
        /// Map to list object
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static IEnumerable<object> MapToListObj(this DbDataReader dr, TypeInfo typeInfo)
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeInfo.AsType();
                var entities = new List<object>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (dr.Read())
                {
                    var newObject = ObjectBuilder.CreateNewObject(typeInfo);
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        if (propDict.ContainsKey(dr.GetName(i).ToUpper()))
                        {
                            var info = propDict[dr.GetName(i).ToUpper()];
                            if (info != null && info.CanWrite)
                            {
                                var val = dr.GetValue(i);
                                info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return new List<object>();
        }
    }
}
