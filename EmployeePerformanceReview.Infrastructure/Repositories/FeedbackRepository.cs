using EmployeePerformanceReview.Application.Repositories.Interfaces;
using EmployeePerformanceReview.Domain.Entities;
using EmployeePerformanceReview.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Infrastructure.Repositories
{
    internal class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        private readonly ApplicationDbContext _db;
        public FeedbackRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Feedback feedback)
        {
            _db.Feedbacks.Update(feedback);
        }
    }
}
