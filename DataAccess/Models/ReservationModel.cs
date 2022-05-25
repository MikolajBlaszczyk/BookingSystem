using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
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
