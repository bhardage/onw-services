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

        private static MongoDbContext _dbContext;

        protected static MongoDbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public static void InitializeDbContext()
        {
            if (_dbContext == null)
            {
                MongoDbRunner runner = MongoDbRunner.Start();

                _dbContext = new MongoDbContext
                {
                    Runner = runner,
                    Database = new MongoClient(runner.ConnectionString).GetDatabase(DATABASE_NAME),
                    DatabaseName = DATABASE_NAME
                };
            }
        }

        public static void DestroyDbContext()
        {
            if (_dbContext != null)
            {
                _dbContext.Runner.Dispose();
                _dbContext = null;
            }
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
