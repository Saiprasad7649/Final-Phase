using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Equinox.Models.Data
{
    public class QueryOptions<T>
    {
        // For filtering
        public Expression<Func<T, bool>>? Where { get; set; }
        public bool HasWhere => Where != null;

        // For ordering
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc";
        public bool HasOrderBy => OrderBy != null;

        // For paging
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;

        // For includes
        private string[]? includes;
        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }
        public string[]? GetIncludes() => includes;
    }
}
