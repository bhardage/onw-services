using ONWServices.Models;
using System;

namespace ONWServices.Repositories
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Game FindByGameId(Guid gameId);

        void DeleteByGameId(Guid gameId);
    }
}
