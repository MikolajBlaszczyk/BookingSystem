namespace DeskBookingSystemAPI.Models
{
    public class ReservationModel
    {
        public int ID { get; set; }
        public int DeskID { get; set; }
        public int UserID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
