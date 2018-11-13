using Mongo2Go;
using MongoDB.Driver;

namespace ONWServices.Tests.Integration.Repositories
{
    public class MongoDbContext
    {
        public MongoDbRunner Runner { get; set; }
        public IMongoDatabase Database { get; set; }
        public string DatabaseName { get; set; }
    }
}
