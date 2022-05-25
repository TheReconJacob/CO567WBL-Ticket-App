using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Models
{
    public class EventDetails
    {
        public int Id { get; set; }
        public string Event_Name { get; set; }
        public string Event_Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public virtual ICollection<BookingTable> Booking { get; set; }
    }
}
