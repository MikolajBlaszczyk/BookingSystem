using DataAccess;
using DeskBookingSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeskBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        public EveryoneController EveryoneController { get; set; }
        public DataProcessor DataProcessor { get; set; }
        public EmployeeController()
        {
            EveryoneController = new EveryoneController();
            DataProcessor = new DataProcessor();
        }
        
        //filtering
        [HttpGet("desk/{locationID}")]
        [Authorize(Roles = "Admin,Employee"  )]
        public async Task<IActionResult> FilterDesksByLocation(int locationID)
        {
            var desksJson = await EveryoneController.GetDesks();
            var desks = JsonConvert.DeserializeObject<List<DeskModel>>(desksJson);
            var results = JsonConvert.SerializeObject((desks.Where(x => x.LocationID == locationID).ToList()));
            // might check if results are null but i will display empty values in frontend 
            return Ok(results);
        }

        //booking
        [HttpPut("desk/book/{id}/{startDate}/{endDate}/{userID}")]
        [Authorize(Roles = "Admin,Employee")]
        //TODO should add a specified behavior when i am the one who booked it
        public async Task<IActionResult> BookDesk(int id, string startDate, string endDate, int UserID)
        {
            //change it so token will recognize what id it is
            DateTime stDate = Convert.ToDateTime(startDate);
            DateTime edDate = Convert.ToDateTime(endDate);
            var reservationsJson = await DataProcessor.GetAllReservationsEmployee();
            var reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(reservationsJson);
            reservations =  reservations.Where(x => x.DeskID == id).ToList();
            if ((edDate - stDate).Days <= 7)
            {
                if (reservations.Count == 0 || (reservations.Count(x=> ((Convert.ToDateTime(x.StartDate) <= stDate && edDate<= Convert.ToDateTime(x.EndDate)) ||
                (Convert.ToDateTime(x.StartDate) <= edDate && edDate <= Convert.ToDateTime(x.EndDate)) ||
                (Convert.ToDateTime(x.StartDate) <= stDate && stDate <= Convert.ToDateTime(x.EndDate)))) == 0))
                {
                    await DataProcessor.InsertDeskReservation(UserID, id, startDate, endDate);
                    return Ok($"you've booked a {id} desk");
                }
                else
                {
                    return BadRequest("You can't booked it because it is already booked");
                }
            }
            return BadRequest("You can't book a desk for more than 7 days");
        }
        //swapping TODO!!!
        [HttpPut("desk/book/change/{oldID}/{newID}/{startDate}/{endDate}/{userID}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task SwapDesks(int oldID, int newID, string startDate, string endDate,string oldStartDate)
        {
            DateTime stDate = Convert.ToDateTime(startDate);
            DateTime edDate = Convert.ToDateTime(endDate);
            DateTime oldstDate =Convert.ToDateTime(oldStartDate);
            if (((edDate - stDate).Days <= 7) && (oldstDate- DateTime.Now).Days !=0)
            {
               
            }
        }
    }
}
