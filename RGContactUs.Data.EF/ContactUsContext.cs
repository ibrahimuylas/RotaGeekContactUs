using Microsoft.EntityFrameworkCore;
using RGContactUs.Domain.Entities;
using System;

namespace RGContactUs.Data.EF
{
    public class ContactUsContext : DbContext
    {
        public ContactUsContext()
        { }

        public ContactUsContext(DbContextOptions<ContactUsContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ContactUs> Contacts { get; set; }

    }
}
