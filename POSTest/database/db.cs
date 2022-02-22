using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POSTest.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.database
{
    public class db: DbContext
    {
        protected readonly IConfiguration Configuration;

        public db(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));

        }

        public DbSet<Item> items { get; set; }

        public DbSet<Size> sizes { get; set; }



    }
}
