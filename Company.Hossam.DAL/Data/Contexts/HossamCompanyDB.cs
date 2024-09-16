using Company.Hossam.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hossam.DAL.Data.Contexts
{
    public class HossamCompanyDB:DbContext
    {
        public HossamCompanyDB(DbContextOptions options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = . ; DataBase = HossamCompany ; Trusted_Connection = true ; TrustServerCertificate = true ;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Employees> Employees { get; set; }

    }
}
