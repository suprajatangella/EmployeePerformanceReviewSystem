using EmployeePerformanceReview.Domain.Entities;
using EmployeePerformanceReview.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Score)
            .HasConversion(
                score => score.Value, // to database (int)
                value => new ReviewScore(value) // from database (int to Score)
            )
            .HasColumnName("Score")
            .IsRequired();

            builder.OwnsOne(r => r.Score, score =>
            {
                score.Property(s => s.Value)
                     .HasColumnName("Score")
                     .IsRequired();
            });
        }
    }
}
