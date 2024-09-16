using Company.Hossam.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hossam.DAL.Data.Configuration
{
    public class EmployeeConfiguration:IEntityTypeConfiguration<Employees>
    {
       

        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn(10, 10);

            builder.Property(x => x.Salary).HasColumnType("Decimal(18,2)");

            builder.HasOne(x=>x.WorkFor)
                   .WithMany(x => x.Employees)
                   .HasForeignKey(e=>e.WorkForId);
        }
    }
}
