using MongoDB.Driver;

namespace ONWServices.Repositories
{
    public interface IOnwDbContext
    {
        IMongoDatabase GetDb();
    }
}
