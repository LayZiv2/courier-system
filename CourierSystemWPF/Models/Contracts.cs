using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSystemWPF.Models
{
    public class Contracts
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string? notes { get; set; }
        public int? clientId { get; set; }
    }
}
