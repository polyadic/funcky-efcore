using System.Linq;
using System.Threading.Tasks;
using Funcky.EntityFrameworkCore.Extensions;
using Funcky.Extensions;
using Funcky.Xunit;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Funcky.EntityFrameworkCore.Test.Extensions.AsyncQueryableExtensions
{
    public sealed class LastOrNoneAsyncTest
    {
        [Fact]
        public async Task ReturnsNoneWhenNoMatchingEntitiesAreFound()
        {
            using var db = new TestContext();
            FunctionalAssert.IsNone(await db.People.LastOrNoneAsync());
        }

        [Fact]
        public async Task ReturnsSomeWhenOneEntityIsFound()
        {
            using var db = new TestContext();

            var insertedPerson = new Person { FirstName = "Jane", LastName = "Doe" };
            await db.People.AddAsync(insertedPerson);
            await db.SaveChangesAsync();

            var person = FunctionalAssert.IsSome(await db.People.LastOrNoneAsync());
            Assert.Equal(insertedPerson.Id, person.Id);
        }

        [Fact]
        public async Task ReturnsSomeWhenAMatchingEntityIsFound()
        {
            using var db = new TestContext();

            var insertedPerson1 = new Person { FirstName = "Jane", LastName = "A" };
            await db.People.AddAsync(insertedPerson1);
            var insertedPerson2 = new Person { FirstName = "Jane", LastName = "B" };
            await db.People.AddAsync(insertedPerson2);
            await db.SaveChangesAsync();

            var person = FunctionalAssert.IsSome(await db.People.OrderBy(p => p.LastName).LastOrNoneAsync(p => p.FirstName == "Jane"));
            Assert.Equal(insertedPerson2.Id, person.Id);
        }

        [Fact]
        public async Task ReturnsTheLastMatchingEntity()
        {
            using var db = new TestContext();

            var insertedPerson1 = new Person { FirstName = "Jane", LastName = "Doe" };
            await db.People.AddAsync(insertedPerson1);
            var insertedPerson2 = new Person { FirstName = "Peter", LastName = "Example" };
            await db.People.AddAsync(insertedPerson2);
            await db.SaveChangesAsync();

            var person = FunctionalAssert.IsSome(await db.People.OrderBy(p => p.LastName).LastOrNoneAsync());
            Assert.Equal(insertedPerson2.Id, person.Id);
        }
    }
}
