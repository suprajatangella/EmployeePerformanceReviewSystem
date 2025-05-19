using EmployeePerformanceReview.Domain.Entities;
using EmployeePerformanceReview.Domain.ValueObjects;
using EmployeePerformanceReview.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for Entities
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Signature> Signatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }
    }
}
