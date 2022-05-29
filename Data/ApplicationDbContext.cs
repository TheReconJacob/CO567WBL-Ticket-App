using CO567WBL_Ticket_App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CO567WBL_Ticket_App.Models.ViewModels;

namespace CO567WBL_Ticket_App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BookingTable> BookingTable { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<CO567WBL_Ticket_App.Models.ViewModels.EventDetailViewModel> EventDetailViewModel { get; set; }
        public DbSet<CO567WBL_Ticket_App.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
