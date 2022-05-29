using DataAccess;
using DeskBookingSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DeskBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        public EveryoneController EveryoneController { get; set; }
        public IDataProcessor DataProcessor { get; set; }
        public EmployeeController(EveryoneController everyoneController, IDataProcessor dataProcessor)
        {
            EveryoneController = everyoneController;
            DataProcessor = dataProcessor;
        }
        
        //filtering
        [HttpGet("desk/{locationID}")]
        [Authorize(Roles = "Admin,Employee"  )]
        //Option is added but frontend deals with regular data
        public async Task<IActionResult> FilterDesksByLocation(int locationID)
        {
            var desksJson = await EveryoneController.GetDesks();
            var desks = JsonConvert.DeserializeObject<List<DeskModel>>(desksJson);
            var results = JsonConvert.SerializeObject((desks.Where(x => x.LocationID == locationID).ToList()));
            return Ok(results);
        }

        //booking
        [HttpPut("desk/book/{id}/{startDate}/{endDate}")]
        [Authorize(Roles = "Admin,Employee")]
       
        public async Task<IActionResult> BookDesk(int id, string startDate, string endDate)
        {
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = identity.Claims;
            var UserID = Convert.ToInt32(user.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber)?.Value);
            DateTime stDate = Convert.ToDateTime(startDate);
            DateTime edDate = Convert.ToDateTime(endDate);
            bool validate = await CheckReservationDays(UserID, stDate, edDate, id);
            if (validate == false)
            {
                if (edDate == stDate)
                {
                    return BadRequest("You can't book desk for 0 days");
                }
                return BadRequest("You can't book desk for more than a week");
            }
            var reservationsJson = await DataProcessor.GetAllReservationsEmployee();
            var reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(reservationsJson);
            reservations =  reservations.Where(x => x.DeskID == id).ToList();
            if ((edDate - stDate).Days <= 7)
            {
                if (reservations.Count == 0 || (reservations.Count(x => ((Convert.ToDateTime(x.StartDate) <= stDate && edDate <= Convert.ToDateTime(x.EndDate)) ||
                (Convert.ToDateTime(x.StartDate) <= edDate && edDate <= Convert.ToDateTime(x.EndDate)) ||
                (Convert.ToDateTime(x.StartDate) <= stDate && stDate <= Convert.ToDateTime(x.EndDate)))) == 0))
                {
                    await DataProcessor.InsertDeskReservation(UserID, id, startDate, endDate);
                    return Ok($"you've booked a desk {id}");
                }
                else if (reservations.FirstOrDefault(x => x.DeskID == id).UserID == UserID)
                {
                    return BadRequest("You have booked it previously");
                }
                else
                {
                    return BadRequest("You can't booked it because it is already booked");
                }
            }
            return BadRequest("You can't book a desk for more than 7 days");
        }
        //swapping 
        [HttpPut("desk/book/change/{oldID}/{newID}/{startDate}/{endDate}/{resID}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> SwapDesks(int oldID, int newID, string startDate, string endDate, int resID)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = identity.Claims;
            var userID = Convert.ToInt32(user.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber)?.Value);
            var res = await DataProcessor.GetAllReservationsAdmin();
            var list = JsonConvert.DeserializeObject<List<ReservationModel>>(res);
            DateTime stDate = Convert.ToDateTime(startDate);
            DateTime edDate = Convert.ToDateTime(endDate);
            bool validate = await CheckReservationDays(userID, stDate, edDate, newID);
            if (validate == false)
            {
                if (edDate == stDate)
                {
                    return BadRequest("You can't book desk for 0 days");
                }
                return BadRequest("You can't book desk for more than a week");
            }
            string oldStartDate = list.FirstOrDefault(x => x.ID == resID).StartDate.ToString();
            string oldEndDate = list.FirstOrDefault(x => x.ID == resID).EndDate.ToString();
            DateTime oldstDate =Convert.ToDateTime(oldStartDate);
            DateTime oldEdDate = Convert.ToDateTime(oldEndDate);
            if (oldEdDate == edDate && oldstDate == stDate)
            {
                return BadRequest("Nothing was changed because dates of reservation are the same as previous");
            }
            var reservationsJson = await DataProcessor.GetAllReservationsEmployee();
            var reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(reservationsJson);
            reservations = reservations.Where(x => x.DeskID == newID).ToList();
            if (((edDate - stDate).Days <= 7) && (oldstDate - DateTime.Now).Days > 0)
            {
                await DataProcessor.DeleteReservation(list.FirstOrDefault(x => x.ID == resID).ID);
                await DataProcessor.InsertDeskReservation(userID, newID, startDate, endDate);
                return Ok("Reservation Changed");
            }
            else if((edDate - stDate).Days > 7)
            {
                return BadRequest("You can't book a desk for more than 7 days");
            }
            else
            {
                return BadRequest("you can't swap a desk less than 24 hours before reservation starts");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles ="Admin,Employee")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await DataProcessor.DeleteReservation(id);
            return Ok("reservation deleted");
        }

        internal async Task<bool> CheckReservationDays(int userID,DateTime newStart, DateTime newEnd, int deskID)
        {
            var json = await DataProcessor.GetAllReservationsAdmin();
            var reservations = JsonConvert.DeserializeObject<List<ReservationModel>>(json);
            var usersReservations = reservations.Where(x => x.UserID == userID && x.DeskID == deskID);
            int days = 0; 
            foreach (var reservation in usersReservations)
            {
                DateTime start = Convert.ToDateTime(reservation.StartDate);
                DateTime end = Convert.ToDateTime(reservation.EndDate);
                days += (end - start).Days;
            }
            days += (newEnd-newStart).Days;
            if (newEnd == newStart)
            {
                return false;
            }
            if (days > 7)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
    }
}
