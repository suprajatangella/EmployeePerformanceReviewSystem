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
    }
}
