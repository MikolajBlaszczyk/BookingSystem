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
        public DataProcessor DataProcessor { get; set; }
       

        public EveryoneController()
        {
            DataProcessor = new DataProcessor();
        }

        [HttpGet("data")]
        [Authorize]
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
                var locationsDesk = listOfDesks.Where(x => x.ID == locations.ID).ToList();
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
        [HttpGet]
        [Authorize]
        public async Task<string> GetReservations()
        {
            string results = await DataProcessor.GetAllReservationsEmployee();
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

       
    }
}
