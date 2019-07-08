using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deliveries.ViewModels
{
    public class DeliveryViewModel
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public string PhoneNumber { get; set; }

        public AddressViewModel From { get; set; }

        public AddressViewModel To { get; set; }
    }
}
