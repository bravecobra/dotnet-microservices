using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deliveries.ViewModels
{
    public class AddressViewModel
    {
        public string ZipCode { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
