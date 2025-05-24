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
    public class GoalRepository : Repository<Goal>, IGoalRepository
    {
        private readonly ApplicationDbContext _db;
        public GoalRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Goal goal)
        {
            _db.Goals.Update(goal);
        }
    }
}
