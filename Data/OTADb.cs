using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTASystem.Data
{
    public class OTADb : IdentityDbContext
    {
        public DbSet<OTAUser> OTAUsers { get; set; }
        public DbSet<OTARecord> OTARecords { get; set; }

        public OTADb(DbContextOptions<OTADb> options)
            : base(options)
        {

        }
    }
}
