using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSystemWPF.Models
{
    public class Delivery
    {
        public int id { get; set; }
        public string destinationAddress { get; set; }
        public DateTime deliveryDateTime { get; set; }
        public int contractId { get; set; }
        public int delivered { get; set; }
        public int? courierId { get; set; }
        public int accepted { get; set; }
    }
}
