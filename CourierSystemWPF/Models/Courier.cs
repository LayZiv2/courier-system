using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSystemWPF.Models
{
    public class Courier
    {
        public int id {  get; set; }
        public int completedOrders { get; set; }
        public int cancelledOrders { get; set; }
        public int employeeId { get; set; }
    }
}
