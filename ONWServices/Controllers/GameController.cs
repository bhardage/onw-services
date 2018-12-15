using Microsoft.AspNetCore.Mvc;
using ONWServices.ViewModels;

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
        [HttpGet]
        public ActionResult<GameViewModel> CreateGame()
        {
            return new ActionResult<GameViewModel>(new GameViewModel() { id = "1234" });
            
        }

        [HttpGet("{id}")]
        public ActionResult<GameViewModel> GetGame(string id)
        {
            return new GameViewModel() { id = id };
        }

        [HttpPost]
        public ActionResult<GameViewModel> UpdateGameSettings(string id, int gameState)    
        {
            return new GameViewModel() { };
        }

        [HttpDelete("{id}")]
        public string CloseGame(string id)
        {
            return "Game Closed";
        }
    }
}