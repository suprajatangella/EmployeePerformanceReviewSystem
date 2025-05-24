using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Domain.Entities
{
    public class Signature
    {
        public Guid Id { get; set; }
        public Guid ReviewId { get; set; }
        public string SignedBy { get; set; }       // Name or EmployeeId
        public string ImagePath { get; set; }      // Saved in wwwroot/uploads/signatures/
        public DateTime SignedDate { get; set; }

        public Review Review { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }            // User who created the follow-up
        public string? UpdatedBy { get; set; }
    }
}
