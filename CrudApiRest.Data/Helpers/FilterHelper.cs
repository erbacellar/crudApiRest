using CrudApiRest.Shared.Common.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CrudApiRest.Data.Helpers
{
    public static class FilterHelper
    {
        private static string orderBy = "asc";

        public static IQueryable<T> AddOrderBy<T>(this IQueryable<T> query, PagingData paging, Expression<Func<T, string>> nomeSelector)
        {
            if (paging.OrderBy != null)
                orderBy = paging.OrderBy;

            if (orderBy.Equals("desc"))
                query = query.OrderByDescending(nomeSelector);
            else
                query = query.OrderBy(nomeSelector);

            return query;
        }

        public static IQueryable<T> AddOrderByCodigo<T>(this IQueryable<T> query, PagingData paging, Expression<Func<T, int>> codigoSelector)
        {
            if (paging.OrderBy != null)
                orderBy = paging.OrderBy;

            if (orderBy.Equals("desc"))
                query = query.OrderByDescending(codigoSelector);
            else
                query = query.OrderBy(codigoSelector);

            return query;
        }

        public static IQueryable<T> AddPagination<T>(this IQueryable<T> query, PagingData paging)
        {
            if (paging == null)
                return query;

            if (!paging.Page.HasValue || paging.Page < 0)
                paging.Page = 0;

            if (paging.PageSize.HasValue)
            {

                if (paging.PageSize < 1)
                    paging.PageSize = 5;

                query = query.Skip(paging.Page.Value * paging.PageSize.Value)
                    .Take(paging.PageSize.Value);
            }

            return query;
        }
    }
}
