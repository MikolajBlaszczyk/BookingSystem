using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
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
