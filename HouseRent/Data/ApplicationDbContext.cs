using HouseRent.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseRent.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<DetailOfFlat> DetailOfFlats { get; set; }
        public DbSet<DetailOfDuplex> DetailOfDuplexs { get; set; }


    }
}
