using System.Collections.Generic;
using System.Linq;

namespace CoreApp.Common.Models
{
    public class PagedList<T>
    {
        #region Fields
        /// <summary>
        /// Default page size
        /// </summary>
        private const int _defaultPageSize = 10;
        #endregion

        #region Contructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Queryable</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IQueryable<T> data, int pageIndex = 1, int pageSize = _defaultPageSize, dynamic extendData = null)
        {
            PageIndex = pageIndex == 0 ? 1 : pageIndex;
            PageSize = pageSize == 0 ? _defaultPageSize : pageSize;
            ExtendData = extendData;

            Items = new List<T>();
            if (data != null && data.Any())
            {
                Items = data.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }

            TotalItemCount = data?.Count() ?? 0;
        }

        public PagedList(IEnumerable<T> data, int pageIndex = 1, int pageSize = _defaultPageSize, dynamic extendData = null)
        {
            PageIndex = pageIndex == 0 ? 1 : pageIndex;
            PageSize = pageSize == 0 ? _defaultPageSize : pageSize;
            ExtendData = extendData;

            Items = new List<T>();
            if (data != null && data.Any())
            {
                Items = data.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }

            TotalItemCount = data?.Count() ?? 0;
        }
        #endregion

        #region Properties

        public IList<T> Items { get; set; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalItemCount { get; }
        public dynamic ExtendData { get; set; }

        #endregion
    }
}
