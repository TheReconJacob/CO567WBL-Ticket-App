using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Models
{
    public class ProjectRole
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
    }
}
