using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Locadora.API.Models;

namespace Locadora.API.FiltersDb
{
    public class FilterHelper
    {
        public static IQueryable<Publishers> ApplyFilter(string filterValue, IQueryable<Publishers> queryable)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(Publishers), "x");
            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            Expression orExpression = null;

            foreach (PropertyInfo propertyInfo in typeof(Publishers).GetProperties())
            {
                MemberExpression propertyExpression = Expression.Property(parameter, propertyInfo);

                Expression filterExpression;

                if (propertyInfo.PropertyType == typeof(string))
                {
                    filterExpression = Expression.Call(propertyExpression, containsMethod, Expression.Constant(filterValue));
                }
                else if (IsNumericType(propertyInfo.PropertyType))
                {
                    // Converte o valor de filtro para o tipo da propriedade antes de comparar
                    object convertedValue = Convert.ChangeType(filterValue, propertyInfo.PropertyType);
                    ConstantExpression constant = Expression.Constant(convertedValue);
                    filterExpression = Expression.Equal(propertyExpression, constant);
                }
                else
                {
                    continue; // Ignora tipos de dados não suportados
                }

                if (orExpression == null)
                {
                    orExpression = filterExpression;
                }
                else
                {
                    orExpression = Expression.OrElse(orExpression, filterExpression);
                }
            }

            if (orExpression == null)
            {
                // Se nenhuma propriedade de comparação for encontrada, retorne uma consulta sem filtro
                return queryable;
            }

            Expression<Func<Publishers, bool>> lambda = Expression.Lambda<Func<Publishers, bool>>(orExpression, parameter);
            return queryable.Where(lambda);
        }

        private static bool TryConvertToType(string input, Type targetType, out object convertedValue)
        {
            try
            {
                if (targetType.IsEnum)
                {
                    convertedValue = Enum.Parse(targetType, input);
                    return true;
                }

                if (targetType == typeof(Guid))
                {
                    convertedValue = Guid.Parse(input);
                    return true;
                }

                if (targetType == typeof(int) || targetType == typeof(long) || targetType == typeof(decimal) || targetType == typeof(double) || targetType == typeof(float))
                {
                    // Verifica se a entrada é um valor numérico válido
                    if (double.TryParse(input, out double numericValue))
                    {
                        convertedValue = Convert.ChangeType(numericValue, targetType);
                        return true;
                    }
                }

                // Para outros tipos, tenta converter diretamente
                convertedValue = Convert.ChangeType(input, targetType);
                return true;
            }
            catch
            {
                convertedValue = null;
                return false;
            }
        }


        private static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                
                case TypeCode.Int32:
                    return true;
                case TypeCode.String:
                    return false;
            }
        }
    }
}
