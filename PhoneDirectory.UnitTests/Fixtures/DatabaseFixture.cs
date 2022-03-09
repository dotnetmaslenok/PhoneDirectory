using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PhoneDirectory.Infrastructure.Database;
using PhoneDirectory.UnitTests.DataHelpers;

namespace PhoneDirectory.UnitTests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public ApplicationDbContext DbContext { get; set; }
        
        public DatabaseFixture()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("PhoneDirectoryInMemory")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            var options = builder.Options;

            DbContext = new ApplicationDbContext(options);

            DbContext.Divisions.Add(DivisionHelper.GetOneDefaultEntity());
            DbContext.Divisions.AddRange(DivisionHelper.GetManyDefaultEntities());
            DbContext.Users.Add(UserHelper.GetOneDefaultEntity());
            DbContext.Users.AddRange(UserHelper.GetManyDefaultEntities());
            DbContext.PhoneNumbers.Add(PhoneNumberHelper.GetOneDefaultEntity());
            DbContext.PhoneNumbers.AddRange(PhoneNumberHelper.GetManyDefaultEntities());

            DbContext.SaveChanges();
        }
        
        public void Dispose()
        {
            
        }
    }
}