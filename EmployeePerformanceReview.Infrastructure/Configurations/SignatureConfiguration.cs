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
    public class SignatureConfiguration : IEntityTypeConfiguration<Signature>
    {
        public void Configure(EntityTypeBuilder<Signature> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SignedBy)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.ImagePath)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(s => s.SignedDate)
                   .IsRequired();

            builder.HasOne(s => s.Review)
                   .WithMany(r => r.Signatures) // Make sure `Review` has `ICollection<Signature> Signatures`
                   .HasForeignKey(s => s.ReviewId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.User)
               .WithMany(u => u.Signatures)
               .HasForeignKey(g => g.UserId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
