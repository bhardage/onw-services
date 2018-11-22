using ONWServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONWServices.Initializers
{
    public class GameInitializer : IModelInitializer<Game>
    {
        public Game Setup(Game game)
        {            
            game.Status = GameStatus.New;
            game.SelectedRoles = new List<Role>
            {
                Role.Warewolf,
                Role.Seer,
                Role.Robber,
                Role.Troublemaker
            };

            return game;
        }
    }
}
