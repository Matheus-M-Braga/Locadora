using System;
using System.Linq;
using System.Linq.Expressions;

namespace Locadora.API.FiltersDb
{
    public static class FilterHelper
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> queryable, string filterValue)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression orExpression = null;

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var property = Expression.Property(parameter, propertyInfo);

                if (TryConvertToType(filterValue, propertyInfo.PropertyType, out object filterConvertedValue))
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var filterExpression = Expression.Call(property, containsMethod, Expression.Constant(filterConvertedValue));
                        orExpression = orExpression == null ? filterExpression : Expression.OrElse(orExpression, filterExpression);
                    }
                    else
                    {
                        var convertedConstant = Expression.Constant(filterConvertedValue);
                        var filterExpression = Expression.Equal(property, convertedConstant);
                        orExpression = orExpression == null ? filterExpression : Expression.OrElse(orExpression, filterExpression);
                    }
                }
            }

            if (orExpression != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(orExpression, parameter);
                return queryable.Where(lambda);
            }

            return queryable;
        }

        private static bool TryConvertToType(string input, Type targetType, out object convertedValue)
        {
            try
            {
                convertedValue = Convert.ChangeType(input, targetType);
                return true;
            }
            catch
            {
                convertedValue = null;
                return false;
            }
        }
    }
}
