using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSystemWPF.Models
{
    public class Breaks
    {
        public int id { get; set; }
        public DateTime? startTime { get; set; }
        public int? courierId { get; set; }
    }
}
