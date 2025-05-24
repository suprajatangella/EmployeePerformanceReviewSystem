using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string ManagerId { get; set; }  // FK to another Employee
        public DateTime DateOfJoining { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }            // User who created the follow-up
        public string? UpdatedBy { get; set; }
    }
}
