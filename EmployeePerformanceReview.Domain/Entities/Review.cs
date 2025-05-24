using EmployeePerformanceReview.Domain.Enums;
using EmployeePerformanceReview.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime ReviewPeriodStart { get; set; }
        public DateTime ReviewPeriodEnd { get; set; }
        public string ReviewerId { get; set; }  // FK to Employee
        public string Comments { get; set; }
        public ReviewStatus Status { get; set; } // Enum: Pending, Submitted, Approved
        public ReviewScore Score { get; set; }
        public DateTime SubmittedDate { get; set; }
        public ICollection<Signature> Signatures { get; set; } 
        public Employee Employee { get; set; }
        //public Signature Signature { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }            // User who created the follow-up
        public string? UpdatedBy { get; set; }
    }
}
