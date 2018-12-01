using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mlh.Models
{
    public class db:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=tcp:random0256.database.windows.net,1433;Initial Catalog=mhl-student;Persist Security Info=False;User ID=random0256;Password=asd45214..;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<User> users { get; set; }
        public DbSet<Course> courses { get; set; }

    }

}
