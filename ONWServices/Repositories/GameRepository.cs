using MongoDB.Driver;
using ONWServices.Models;
using System;
using System.Linq;

namespace ONWServices.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        private const string GAME_COLLECTION_NAME = "games";

        public GameRepository(IOnwDbContext context) : base(context, GAME_COLLECTION_NAME)
        {
        }

        public Game FindByGameId(Guid gameId)
        {
            FilterDefinition<Game> filter = Builders<Game>.Filter
                .Eq(g => g.GameId, gameId);

            return GetCollection()
                .Find(filter)
                .FirstOrDefault();
        }

        public void DeleteByGameId(Guid gameId)
        {
            FilterDefinition<Game> filter = Builders<Game>.Filter
                .Eq(g => g.GameId, gameId);

            GetCollection()
                .DeleteOne(filter);
        }
    }
}
