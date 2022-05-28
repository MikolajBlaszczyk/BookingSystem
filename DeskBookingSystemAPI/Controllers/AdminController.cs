using DataAccess;
using DeskBookingSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeskBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IDataProcessor DataProcessor { get; set; }
        private EveryoneController EveryoneController { get; set; }
        public AdminController(EveryoneController everyoneController, IDataProcessor dataProcessor)
        {
            DataProcessor = dataProcessor;
            EveryoneController = everyoneController;
        }
        // can't delete the location if it has any desks in it
        [HttpDelete("location/remove/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var desks = await EveryoneController.GetDesks();
            var listofDesks = JsonConvert.DeserializeObject<List<DeskModel>>(desks) ;
            if (listofDesks.Count(desk => desk.LocationID == id) == 0)
            {
                await DataProcessor.DeleteLocation(id);
                return Ok("location has been removed");
            }
            else
            {
                return BadRequest("location can't be removed due to desks that exists in it");
            }
        }
        
        [HttpDelete("desk/remove/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDesk(int id)
        {
            var json = await EveryoneController.GetReservations();
            var reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(json);
            if (reservations.FirstOrDefault(x => x.DeskID == id) == null)
            {
                await DataProcessor.DeleteDesk(id);
                return Ok("desk has been removed");
            }
            else
            {
                return BadRequest("desk is reserved");
            }
        }

        [HttpPut("location/add/{address}/{city}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddLocation(string address, string city)
        {
            await DataProcessor.AddLocation(address, city);
            return Ok("location added");
        }
        
        [HttpPut("desk/add/{position}/{monitors}/{locationID}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDesk(string position, int monitors, int locationID)
        {
            var locationsJson = await EveryoneController.GetLocations();
            var locations = JsonConvert.DeserializeObject<List<LocationModel>>(locationsJson);
            if (locations.FirstOrDefault(x => x.ID == locationID) is not null)
            {
                DataProcessor.AddDesk(position, monitors, locationID);  
                return Ok("desk added");
            }
            else
            {
                return BadRequest("location doesn't exist");
            }
            
        }

        
    }
}
