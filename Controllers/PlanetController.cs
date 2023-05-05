using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAnLapTrinhWebNC.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoAnLapTrinhWebNC.Controllers
{
    //[Route("he-mat-troi/[action]")]
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetservice;
        private readonly ILogger<PlanetController> _logger;

        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetservice = planetService;
            _logger = logger;
        }

        [Route("danh-sach-cac-hanh-tinh.html")]
        public IActionResult Index()
        {
            return View();
        }
        //route : action
        [BindProperty(SupportsGet = true, Name = "action")]
        public string Name { set; get; }//action ~ PlanetModel
        public IActionResult Mercury()
        {
            var planet = _planetservice.Where(p => p.Name == Name).FirstOrDefault();
            //truyen qua model
            return View("Detail", planet);
        }

        public IActionResult Venus()
        {
            var planet = _planetservice.Where(p => p.Name == Name).FirstOrDefault();
            //truyen qua model
            return View("Detail", planet);
        }
        public IActionResult Mars()
        {
            var planet = _planetservice.Where(p => p.Name == Name).FirstOrDefault();
            //truyen qua model
            return View("Detail", planet);
        }
        [Route("sao/[controller]/[action]", Order = 2, Name = "Earth2")] //sao/Plainet/Earth
        [Route("[controller]-[action].html", Order = 1, Name = "Earth1")]//Planet-Earth.html
        public IActionResult Earth()
        {
            var planet = _planetservice.Where(p => p.Name == Name).FirstOrDefault();
            //truyen qua model
            return View("Detail", planet);
        }
        [HttpGet("saomoc.html")]
        public IActionResult Jupiter()
        {
            var planet = _planetservice.Where(p => p.Name == Name).FirstOrDefault();
            //truyen qua model
            return View("Detail", planet);
        }

        [Route("hanhtinh/{id:int}")]
        public IActionResult PlanetInfo(int id)
        {
            var planet = _planetservice.Where(p => p.Id == id).FirstOrDefault();
            //truyen qua model
            return View("Detail", planet);
        }
    }
}