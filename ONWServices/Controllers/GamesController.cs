using Microsoft.AspNetCore.Mvc;
using ONWServices.Initializers;
using ONWServices.Mappers;
using ONWServices.Models;
using ONWServices.Repositories;
using ONWServices.ViewModels;
using System;

namespace ONWServices.Controllers
{

    //Game Flow
    //
    //User Creates game, becomes Game Master, tells Players Game ID
    //Master sets up game settings:  Timer length, Available Roles,
    //Available roles based on number of players (# number of players + 3)
    //Players join game
    //when all players have joined, Master initiates Night Phase
    //Players see their role
    //Players submit their actions by choosing players based on their role
    //When all turns are submitted, Master initiates Information Phase
    //This turns on a game timer
    //players sees their new information, discuss what they saw, choose who to shoot
    //At the end of time timer, the game tallies who is shot and announces winner  

    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _repo;
        private readonly GameInitializer _initializer;
        private readonly GameMapper _mapper;

        public GamesController(IGameRepository repo, GameInitializer initializer, GameMapper mapper)
        {
            this._repo = repo;
            this._initializer = initializer;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<GameViewModel> CreateGame()
        {            
            var game = new Game();
            game = _initializer.Setup(game);
            _repo.Save(game);

            var dm = _repo.FindByGameId(game.GameId);
            var vm = _mapper.ToViewModel(dm);
            return new ActionResult<GameViewModel>(vm);            
        }

        [HttpGet("{id}")]
        public ActionResult<GameViewModel> GetGame(string id)
        {
            var game = _repo.FindByGameId(new Guid(id));
            return _mapper.ToViewModel(game);           
        }

        [HttpPost]
        public ActionResult<GameViewModel> UpdateGameSettings(GameViewModel vm)
        {
            var newDm = _mapper.ToDomainModel(vm);
            _repo.Save(newDm);
            return _mapper.ToViewModel(newDm);
        }

        [HttpDelete("{id}")]
        public string CloseGame(string id)
        {
            var game = _repo.FindByGameId(new Guid(id));
            game.Status = GameStatus.Closed;
            _repo.Save(game);
            return "Game Closed";
        }
    }
}