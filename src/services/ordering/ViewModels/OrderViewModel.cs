using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ordering.Models;

namespace ordering.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public OrderStates State { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
