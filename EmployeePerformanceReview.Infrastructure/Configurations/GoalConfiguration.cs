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
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(g => g.Description)
                   .HasMaxLength(500);

            builder.Property(g => g.Category)
                   .HasMaxLength(50);

            builder.Property(g => g.Status)
                   .IsRequired()
                   .HasConversion<string>(); // stores enum as string

            builder.Property(g => g.TargetDate)
                   .IsRequired();

            builder.Property(g => g.Remarks)
                   .HasMaxLength(500);

            builder.HasOne(g => g.Employee)
                   .WithMany(e => e.Goals)
                   .HasForeignKey(g => g.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.User)
               .WithMany(u => u.Goals)
               .HasForeignKey(g => g.UserId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
