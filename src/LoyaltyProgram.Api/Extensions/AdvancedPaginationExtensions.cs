using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

public class PagedResult<T>
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
    [JsonPropertyName("total_pages")]
    public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);
    [JsonPropertyName("items")]
    public List<T> Items { get; set; } = new List<T>();
}

public static class IQueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize,
        string? sortBy = null,
        bool ascending = true,
        string? search = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        Expression<Func<T, DateTime?>>? dateSelector = null,
        params Expression<Func<T, string>>[] searchFields
        )
    {
        if (!string.IsNullOrWhiteSpace(search) && searchFields.Any())
        {
            var parameter = Expression.Parameter(typeof(T), "t");
            Expression? predicate = null;
            foreach (var searchField in searchFields)
            {
                var containsExpression = Expression.Call(
                    Expression.Invoke(searchField, parameter),
                    typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                    Expression.Constant(search, typeof(string))
                );
                predicate = predicate == null ? containsExpression : Expression.OrElse(predicate, containsExpression);
            }
            var lambda = Expression.Lambda<Func<T, bool>>(predicate!, parameter);
            query = query.Where(lambda);
        }
        if (dateSelector != null && (startDate.HasValue || endDate.HasValue))
        {
            if (startDate.HasValue)
            {
                query = query.Where(x => EF.Property<DateTime?>(x!, dateSelector.GetPropertyName()) >= startDate);


            }
            if (endDate.HasValue)
            {
                 query = query.Where(x => EF.Property<DateTime?>(x!, dateSelector.GetPropertyName()) <= endDate);
            }
        }

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = query.OrderByDynamic(sortBy, ascending);
        }

        int totalItemCount;
        List<T> items;
        if (query.Provider is IAsyncQueryProvider)
        {
            totalItemCount = await query.CountAsync();
            items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        else
        {
            totalItemCount = query.Count();
            items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        return new PagedResult<T>
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalItemCount,
            Items = items
        };
        
    }

    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool ascending)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, propertyName);
        var sortExpression = Expression.Lambda(property, parameter);

        string method = ascending ? "OrderBy" : "OrderByDescending";
        var resultExpression = typeof(Queryable).GetMethods().First(m => m.Name == method && m.GetParameters().Length == 2).MakeGenericMethod(typeof(T), property.Type).Invoke(null, new object[] { query, sortExpression });

        return (IQueryable<T>)resultExpression!;
    }

    public static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
    {
        if (expression is MemberExpression member)
        {
            return member.Member.Name;
        }

        if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        throw new InvalidOperationException("Expression must be a member expression.");
    
    }
}