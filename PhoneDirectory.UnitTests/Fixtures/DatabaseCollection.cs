using Xunit;

namespace PhoneDirectory.UnitTests.Fixtures
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        
    }
}