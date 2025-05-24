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
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _db;
        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Review review)
        {
            _db.Reviews.Update(review);
        }
    }
}
