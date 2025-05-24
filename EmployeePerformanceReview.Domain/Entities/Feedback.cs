using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Domain.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }      // Who the feedback is about
        public string FeedbackText { get; set; }
        public DateTime DateGiven { get; set; }
        public Employee Employee { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }            // User who created the follow-up
        public string? UpdatedBy { get; set; }
    }
}
