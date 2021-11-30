using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoweWorkDb.Models
{
    public class WorkPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string Place { get; set; }
        public int ZipCode { get; set; }
        public string Voivodeship { get; set; }
    }
}
