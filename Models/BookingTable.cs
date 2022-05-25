using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Models
{
    public class BookingTable
    {
        public int Id { get; set; }
        public string Seat_No { get; set; }
        public string User_Id { get; set; }
        public DateTime DateToPresent { get; set; }
        public int EventDetailsId { get; set; }
        public int Amount { get; set; }
        [ForeignKey("EventDetailsId")]
        public virtual EventDetails eventDetails { get; set; }
    }
}
