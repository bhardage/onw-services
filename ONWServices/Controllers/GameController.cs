using Microsoft.AspNetCore.Mvc;
using ONWServices.ViewModels;

namespace ONWServices.Controllers
{

    //Game statuses: New, Available, Night Phase, Information Review, Discussion, Round Summary, (Back to Night Phase), Game Summary, Closed
    //Game must have a minimum of 3 players
    //Game must have 1 Warewolf, Seer, Robber, and Troublemaker
    //Game can only have 1 Game Master


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

        [HttpDelete("{id}")]
        public string CloseGame(string id)
        {
            return "Game Closed";
        }
    }
}