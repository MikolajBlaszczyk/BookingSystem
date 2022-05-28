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
    public class EveryoneController : ControllerBase
    {
        public IDataProcessor DataProcessor { get; set; }


        public EveryoneController(IDataProcessor dataProcessor)
        {
            DataProcessor = dataProcessor;
        }

        [HttpGet("data")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<string> GetData()
        {
            string locations = await DataProcessor.GetAllLocations();
            string desks = await DataProcessor.GetAllDesks();
            var listOfLocations = JsonConvert.DeserializeObject<List<DataAccess.Models.LocationModel>>(locations);
            var listOfDesks = JsonConvert.DeserializeObject<List<DeskModel>>(desks);
            var groupedList = groupDesks(listOfDesks, listOfLocations);
            var result = JsonConvert.SerializeObject(groupedList);
            return result;
        }

        private List<LocationModel> groupDesks(List<DeskModel>? listOfDesks, List<DataAccess.Models.LocationModel>? listOfLocations)
        {
            List<LocationModel> output = new();
            foreach (var locations in listOfLocations)
            {
                var locationsDesk = listOfDesks.Where(x => x.LocationID == locations.ID).ToList();
                output.Add(new LocationModel
                {
                    ID = locations.ID,
                    Address = locations.Address,
                    City = locations.City,
                    NumberOfDesks = locations.NumberOfDesks,
                    AllReserved = locations.AllReserved,
                    Desks = locationsDesk
                });
            }
            return output;
        }


        //?
        [HttpGet("Reservations")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<string> GetReservations()
        {
            string json = await DataProcessor.GetAllReservationsEmployee();
            string results = await GroupDesks(json, false);
            return results;
        }


        [HttpGet("Reservations/Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<string> GetReservationsAdmin()
        {
            string json = await DataProcessor.GetAllReservationsAdmin();
            string results = await GroupDesks(json, true);
            return results;
        }

        [HttpGet("locations")]
        [Authorize]
        public async Task<string> GetLocations()
        {
            string results = await DataProcessor.GetAllLocations();
            return results;
        }

        [HttpGet("desks")]
        [Authorize]
        public async Task<string> GetDesks()
        {
            string results = await DataProcessor.GetAllDesks();
            return results;
        }
        [HttpGet("Users/{username}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<string> GetUser(string username)
        {
            var user = await DataProcessor.GetAllUsers();
            return JsonConvert.SerializeObject(user.FirstOrDefault(x=>x.Username == username));
        }

        protected async Task<string> GroupDesks(string json, bool admin)
        {
            var desks = JsonConvert.DeserializeObject<List<DeskModel>>(await GetDesks());
            var reservation = JsonConvert.DeserializeObject<List<ReservationModel>>(json);
            List<DeskModel> groupedDesks = new();
            string result;
            if (admin is true)
            {
                var list = reservation.Join(desks,
                   res => res.DeskID,
                   desk => desk.ID,
                   (res, desk) => new { ID=desk.ID, IsReserved = true, UserID = res.UserID, Position = desk.Position, Monitors = desk.Monitors, LocationID = desk.LocationID, Start = res.StartDate, End = res.EndDate, resID=res.ID });
                result = JsonConvert.SerializeObject(list);
            }
            else
            {
                var list = reservation.Join(desks,
                   res => res.DeskID,
                   desk => desk.ID,
                   (res, desk) => new { ID = desk.ID, IsReserved = true, Position = desk.Position, Monitors = desk.Monitors, LocationID = desk.LocationID, Start = res.StartDate, End=res.EndDate, resID = res.ID });
                result = JsonConvert.SerializeObject(list);
            }
            
            return result;
        }
       
    }
}
