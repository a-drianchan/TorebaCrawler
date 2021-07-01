using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TorebaCrawlerCore;

namespace TorebaCrawlerWeb.Data
{
    public class TorebaCrawlerWebContext : DbContext
    {
        public TorebaCrawlerWebContext (DbContextOptions<TorebaCrawlerWebContext> options)
            : base(options)
        {
        }

        public DbSet<TorebaCrawlerCore.Machine> Machine { get; set; }

        public DbSet<TorebaCrawlerCore.Replay> Replay { get; set; }
    }
}
