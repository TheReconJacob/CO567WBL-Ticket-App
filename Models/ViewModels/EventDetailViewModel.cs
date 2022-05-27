using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Models.ViewModels
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfMovie { get; set; }
        public string EventPicture { get; set; }
    }
}
