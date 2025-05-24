using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }       // Example: Supraja M
        public string LinkedInUrl { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Signature> Signatures { get; set; }
        public string ProfilePictureUrl { get; set; } // URL to the user's profile picture
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateTime? UpdatedDate { get; set; }
    }
}
