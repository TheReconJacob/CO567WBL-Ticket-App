using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Models.ViewModels
{
    public class BookNowViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Event_Name { get; set; }
        public DateTime Event_Date { get; set; }
        public string Seat_No { get; set; }
        public int Amount { get; set; }
        public int Event_Id { get; set; }
    }
}
