using EmployeePerformanceReview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(e => e.Reviews)
                .WithOne(r => r.Employee)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Goals)
                .WithOne(g => g.Employee)
                .HasForeignKey(g => g.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Feedbacks)
       .WithOne(f => f.Employee)
       .HasForeignKey(f => f.EmployeeId)
       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
