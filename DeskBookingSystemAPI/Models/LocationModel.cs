namespace DeskBookingSystemAPI.Models
{
    public class LocationModel
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int NumberOfDesks { get; set; }
        public bool AllReserved { get; set; }
        public List<DeskModel> Desks { get; set; } = new List<DeskModel>();

    }
}
