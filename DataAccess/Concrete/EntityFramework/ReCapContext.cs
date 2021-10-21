using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class ReCapContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ReCap;Trusted_Connection=true");  //sqlserver'a nasıl bağlanacağımı belirttim. (@"Server=175.45.2.12"),Ip SQL SERVERIN NEREDE OLDUĞU SÖYLER. domain güçlü olmayan yerlerde Trusted_Connection=true yerine kullanıcı adı ve şifre isteriz.
        }

        public DbSet<Car> Cars{ get; set; }
        public DbSet<Color> Colors{ get; set; }
        public DbSet<Brand> Brands{ get; set; }
    }
}
