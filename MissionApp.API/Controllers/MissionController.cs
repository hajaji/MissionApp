using Microsoft.AspNetCore.Mvc;
using MissionApp.Service;
using System;
using System.Threading.Tasks;
using MissionApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MissionApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private IMissionService _missionService;
        public MissionController(IMissionService missionService)
        {
            _missionService = missionService; ;
        }

        [HttpPost]
        [Route("AddMission")]
        public async Task<IActionResult> AddMission([FromBody] MissionRequest missionRequest)
        {
            bool result = await _missionService.AddMission(missionRequest);
            if (result)
                return Ok(new { message = "Mission was created" });
            else
                return StatusCode(500, "An error occured");
        }


        [HttpGet]
        [Route("GetCountryByIsolation")]
        public async Task<IActionResult> GetCountryByIsolation()
        {
            (string country, int degreeOfIsolation) = await _missionService.GetCountryByIsolation();

            if (string.IsNullOrEmpty(country))
            {
                return NotFound();
            }

            return Ok(new { Country = country, degreeOfIsolation = degreeOfIsolation });
        }


        [HttpPost]
        [Route("FindClosestMission")]
        public async Task<IActionResult> FindClosestMission([FromBody] AddressRequest address)
        {
            (MissionResponse missionResponse, double distance) = await _missionService.FindClosestMission(address.TargetLocation);
            if (missionResponse == null)
            {
                return NotFound(address.TargetLocation);
            }

            return Ok(new { Mission = missionResponse, distanceInMile = Math.Round(distance, 2) });
        }
    }
}
