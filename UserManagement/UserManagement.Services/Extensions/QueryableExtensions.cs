using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace UserManagement.Services.Extensions;

public static class QueryableExtensions
{

    public static IQueryable<T> OrderByDirection<T>(this IQueryable<T> qry, [NotNull]Expression<Func<T, object>> orderBy, bool ascending)
        => ascending
            ? qry.OrderBy(orderBy)
            : qry.OrderByDescending(orderBy);
}