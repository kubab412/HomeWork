using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoweWorkDb.Models
{
    public class Relations
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int WorkPlaceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
    }
}
