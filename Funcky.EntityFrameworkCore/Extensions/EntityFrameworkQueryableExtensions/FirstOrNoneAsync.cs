using System.Linq.Expressions;
using Funcky.Monads;
using Microsoft.EntityFrameworkCore;

namespace Funcky.Extensions;

public static partial class EntityFrameworkQueryableExtensions
{
    /// <summary>
    /// Returns the first element of a sequence as an <see cref="Option" />, or a <see cref="Option{T}.None" /> value if the sequence contains no elements.
    /// </summary>
    /// <typeparam name="TSource">the inner type of the queryable.</typeparam>
    public static async Task<Option<TSource>> FirstOrNoneAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
        where TSource : notnull
        => await source
            .Select(x => Option.Some(x))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Returns the first element of the sequence as an <see cref="Option{T}" /> that satisfies a condition or a <see cref="Option{T}.None" /> value if no such element is found.
    /// </summary>
    /// <typeparam name="TSource">the inner type of the queryable.</typeparam>
    public static async Task<Option<TSource>> FirstOrNoneAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
        where TSource : notnull
        => await source
            .Where(predicate)
            .Select(x => Option.Some(x))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
}
