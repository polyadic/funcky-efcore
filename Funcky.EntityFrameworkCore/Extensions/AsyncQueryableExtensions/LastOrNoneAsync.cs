using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Funcky.Monads;
using Microsoft.EntityFrameworkCore;

namespace Funcky.EntityFrameworkCore.Extensions
{
    public static partial class AsyncQueryableExtensions
    {
        /// <summary>
        /// Returns the last element of a sequence as an <see cref="Option" />, or a <see cref="Option{T}.None" /> value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">the inner type of the queryable.</typeparam>
        public static Task<Option<TSource>> LastOrNone<TSource>(this IQueryable<TSource> source)
            where TSource : notnull
            => source
                .Select(x => Option.Some(x))
                .LastOrDefaultAsync();

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition as an <see cref="Option{T}" />  or a <see cref="Option{T}.None" /> value if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">the inner type of the queryable.</typeparam>
        public static Task<Option<TSource>> LastOrNoneAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
            where TSource : notnull
            => source
                .Where(predicate)
                .Select(x => Option.Some(x))
                .LastOrDefaultAsync();
    }
}
