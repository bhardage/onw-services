using Microsoft.AspNetCore.Mvc;
using ONWServices.ViewModels;

namespace ONWServices.Controllers
{
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