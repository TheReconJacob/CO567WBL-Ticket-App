using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Seat_No { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int EventId { get; set; }
    }
}
