using Funcky.EntityFrameworkCore.Extensions;
using Funcky.Xunit;
using Xunit;

namespace Funcky.EntityFrameworkCore.Test.Extensions.AsyncQueryableExtensions
{
    public sealed class SingleOrNoneAsyncTest
    {
        [Fact]
        public async Task ReturnsNoneWhenNoMatchingEntitiesAreFound()
        {
            using var db = new TestContext();
            FunctionalAssert.IsNone(await db.People.SingleOrNoneAsync());
        }

        [Fact]
        public async Task ReturnsSomeWhenOneEntityIsFound()
        {
            using var db = new TestContext();

            var insertedPerson = new Person { FirstName = "Jane", LastName = "Doe" };
            await db.People.AddAsync(insertedPerson);
            await db.SaveChangesAsync();

            var person = FunctionalAssert.IsSome(await db.People.SingleOrNoneAsync());
            Assert.Equal(insertedPerson.Id, person.Id);
        }

        [Fact]
        public async Task ReturnsSomeWhenAMatchingEntityIsFound()
        {
            using var db = new TestContext();

            var insertedPerson1 = new Person { FirstName = "Jane", LastName = "Doe" };
            await db.People.AddAsync(insertedPerson1);
            var insertedPerson2 = new Person { FirstName = "Peter", LastName = "Example" };
            await db.People.AddAsync(insertedPerson2);
            await db.SaveChangesAsync();

            var person = FunctionalAssert.IsSome(await db.People.SingleOrNoneAsync(p => p.FirstName == "Jane"));
            Assert.Equal(insertedPerson1.Id, person.Id);
        }

        [Fact]
        public async Task ThrowsWhenMoreThanOneEntitiesAreFound()
        {
            using var db = new TestContext();

            var insertedPerson1 = new Person { FirstName = "Jane", LastName = "Doe" };
            await db.People.AddAsync(insertedPerson1);
            var insertedPerson2 = new Person { FirstName = "Peter", LastName = "Example" };
            await db.People.AddAsync(insertedPerson2);
            await db.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await db.People.SingleOrNoneAsync());
        }

        [Fact]
        public async Task ThrowsWhenMoreThanOneMatchingEntitiesAreFound()
        {
            using var db = new TestContext();

            var insertedPerson1 = new Person { FirstName = "Jane", LastName = "Doe" };
            await db.People.AddAsync(insertedPerson1);
            var insertedPerson2 = new Person { FirstName = "Jane", LastName = "Example" };
            await db.People.AddAsync(insertedPerson2);
            var insertedPerson3 = new Person { FirstName = "Peter", LastName = "Example" };
            await db.People.AddAsync(insertedPerson3);
            await db.SaveChangesAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await db.People.SingleOrNoneAsync(p => p.FirstName == "Jane"));
        }
    }
}
