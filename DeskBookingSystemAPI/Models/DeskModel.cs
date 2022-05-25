namespace DeskBookingSystemAPI.Models
{
    public class DeskModel
    {
        public int ID { get; set; }
        public bool IsReserved { get; set; }
        public int UserID { get; set; }
        public string Position { get; set; }
        public int Monitors { get; set; }
        public int LocationID { get; set; }
    }
}
