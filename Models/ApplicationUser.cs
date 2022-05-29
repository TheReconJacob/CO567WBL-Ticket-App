using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Models
{
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {
    }
}
