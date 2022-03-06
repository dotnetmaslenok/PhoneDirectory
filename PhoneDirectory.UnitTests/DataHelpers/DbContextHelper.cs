using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Infrastructure.Database;

namespace PhoneDirectory.UnitTests.DataHelpers
{
    public class DbContextHelper
    {
        public ApplicationDbContext Context { get; set; }

        public DbContextHelper()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("PhoneDirectoryInMemory")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            var options = builder.Options;

            Context = new ApplicationDbContext(options);

            Context.Divisions.Add(DivisionHelper.GetOne());
            Context.Users.Add(UserHelper.GetOne());
            Context.PhoneNumbers.Add(PhoneNumberHelper.GetOne());

            Context.SaveChanges();
        }
    }
}