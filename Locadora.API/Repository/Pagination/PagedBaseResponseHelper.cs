using System.Linq.Expressions;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Repository
{
    public static class PagedBaseResponseHelper
    {
        public static async Task<TResponse> GetResponseAsync<TResponse, T>(IQueryable<T> query, PagedBaseRequest request) where TResponse : PagedBaseResponse<T>, new()
        {
            var response = new TResponse();
            var count = await query.CountAsync();
            response.TotalPages = (int)Math.Ceiling((double)count / request.PageSize);
            response.TotalRegisters = count;
            if (string.IsNullOrEmpty(request.OrderBy))
                response.Data = await query.ToListAsync();
            else
                response.Data = query.OrderByDynamic(request.OrderBy)
                                     .Skip((request.Page - 1) * request.PageSize)
                                     .Take(request.PageSize)
                                     .ToList();
            return response;
        }
        private static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string propertyName)
        {
            return query.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
        }
    }
}
