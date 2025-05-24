using EmployeePerformanceReview.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Infrastructure.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.FeedbackText)
                   .IsRequired()
                   .HasMaxLength(1000); // You can adjust the limit as needed

            builder.Property(f => f.DateGiven)
                   .IsRequired();

            builder.HasOne(f => f.Employee)
                   .WithMany(e => e.Feedbacks) // Assumes Employee has ICollection<Feedback> Feedbacks
                   .HasForeignKey(f => f.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.User)
               .WithMany(u => u.Feedbacks)
               .HasForeignKey(g => g.UserId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
