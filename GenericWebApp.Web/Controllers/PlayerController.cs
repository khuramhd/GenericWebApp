using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericWebApp.Business;
using Microsoft.AspNetCore.Mvc;

namespace GenericWebApp.Web.Controllers
{
    [Route("players")]
    public class PlayerController : Controller
    {
        private IGameService _gameService;
        public PlayerController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("all")]
        public IActionResult Index()
        {
            return View(_gameService.GetAllPlayers());
        }
    }
}