#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClientApp.Models;

namespace ClientApp.Data
{
    public class ClientAppContext : DbContext
    {
        public ClientAppContext (DbContextOptions<ClientAppContext> options)
            : base(options)
        {
        }

        public DbSet<ClientApp.Models.Client> Clients { get; set; }
    }
}
