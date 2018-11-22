using ONWServices.Models;
using ONWServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONWServices.Mappers
{
    public class GameMapper : IModelMapper<Game, GameViewModel>
    {
        public GameViewModel ToViewModel(Game domain)
        {
            var vm = new GameViewModel
            {
                gameId = domain.GameId.ToString(),
                status = domain.Status
            };

            return vm;
        }

        public Game ToDomainModel(GameViewModel vm)
        {
            var domain = new Game
            {
                GameId = new Guid(vm.gameId),
                Status = vm.status
            };

            return domain;
        }
    }
}
