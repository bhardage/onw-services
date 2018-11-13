using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ONWServices.Settings;

namespace ONWServices.Repositories
{
    public class OnwDbContext : IOnwDbContext
    {
        private readonly IMongoDatabase _db;

        public OnwDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _db = client.GetDatabase(settings.Value.Database);
        }

        public IMongoDatabase GetDb()
        {
            return _db;
        }
    }
}
