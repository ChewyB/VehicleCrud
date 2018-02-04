using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vehicleRESTapi.Models
{
    public class Vehicle
    {
        public long Id { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}