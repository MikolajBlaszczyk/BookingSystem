using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class LocationModel
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string City  { get; set; }
        public int NumberOfDesks { get; set; }
        public bool AllReserved { get; set; }
    }

}
