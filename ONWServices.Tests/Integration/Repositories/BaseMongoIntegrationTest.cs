using Microsoft.Extensions.Options;
using Mongo2Go;
using MongoDB.Driver;
using ONWServices.Repositories;
using ONWServices.Settings;

namespace ONWServices.Tests.Integration.Repositories
{
    public abstract class BaseMongoIntegrationTest
    {
        private const string DATABASE_NAME = "testdb";

        protected static MongoDbContext CreateMongoDbContext()
        {
            MongoDbRunner runner = MongoDbRunner.Start();

            return new MongoDbContext
            {
                Runner = runner,
                Database = new MongoClient(runner.ConnectionString).GetDatabase(DATABASE_NAME),
                DatabaseName = DATABASE_NAME
            };
        }

        protected OnwDbContext CreateOnwDbContext(MongoDbContext dbContext)
        {
            IOptions<MongoDbSettings> settings = Options.Create<MongoDbSettings>(
                new MongoDbSettings
                {
                    ConnectionString = dbContext.Runner.ConnectionString,
                    Database = dbContext.DatabaseName
                }
            );

            return new OnwDbContext(settings);
        }
    }
}
