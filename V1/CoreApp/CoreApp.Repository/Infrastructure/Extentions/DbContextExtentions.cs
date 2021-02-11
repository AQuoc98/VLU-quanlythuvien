using CoreApp.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoreApp.Repository.Infrastructure.Extentions
{
    public static class DbContextExtentions
    {
        public static void Detached<TContext, TEntity>(this TContext context, TEntity entity)
            where TEntity : class
            where TContext : DbContext
        {
            var keyFieldNames = context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(s => s.Name).ToArray();
            var keyValues = new List<object>();
            foreach (var keyName in keyFieldNames)
            {
                var value = ObjectBuilder.GetValueOfObject<object>(entity, keyName);
                keyValues.Add(value);
            }
            
            var localEntity = context.Set<TEntity>().Find(keyValues.ToArray());
            if (localEntity != null)
                context.Entry(localEntity).State = EntityState.Detached;
        }
    }
}
