using System.Linq.Expressions;
using Funcky.Monads;
using Microsoft.EntityFrameworkCore;

namespace Funcky.Extensions;

public static partial class EntityFrameworkQueryableExtensions
{
    /// <summary>
    /// Returns the only element of a sequence as an <see cref="Option" />, or a <see cref="Option{T}.None" /> value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
    /// </summary>
    /// <typeparam name="TSource">the inner type of the queryable.</typeparam>
    public static async Task<Option<TSource>> SingleOrNoneAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
        where TSource : notnull
        => await source
            .Select(x => Option.Some(x))
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Returns the only element of a sequence that satisfies a specified condition as an <see cref="Option{T}" /> or a <see cref="Option{T}.None" /> value if no such element exists; this method throws an exception if more than one element satisfies the condition.
    /// </summary>
    /// <typeparam name="TSource">the inner type of the queryable.</typeparam>
    public static async Task<Option<TSource>> SingleOrNoneAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
        where TSource : notnull
        => await source
            .Where(predicate)
            .Select(x => Option.Some(x))
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
}
