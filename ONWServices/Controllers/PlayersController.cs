using Microsoft.AspNetCore.Mvc;
using ONWServices.ViewModels;

namespace ONWServices.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<PlayerViewModel> GetPlayer(string id)
        {
            return new ActionResult<PlayerViewModel>(new PlayerViewModel() { id = id, name = "Test" });
        }

        [HttpPost("{name}")]
        public ActionResult<PlayerViewModel> CreatePlayer(string name)
        {
            return new ActionResult<PlayerViewModel>(new PlayerViewModel() { id = "1234", name = name });
        }

        [HttpDelete("{id}")]
        public string RemovePlayer(string id)
        {
            return "Player Removed";
        }
    }
}