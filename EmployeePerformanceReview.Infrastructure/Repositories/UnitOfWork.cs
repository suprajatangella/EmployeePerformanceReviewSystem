using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeePerformanceReview.Application.Repositories.Interfaces;
using EmployeePerformanceReview.Infrastructure.Data;
using EmployeePerformanceReview.Infrastructure.Repositories;

namespace EmployeePerformanceReview.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; private set; }

        public IEmployeeRepository Employee { get; private set; }

        public IFeedbackRepository Feedback { get; private set; }

        public IGoalRepository Goal { get; private set; }

        public IReviewRepository Review { get; private set; }

        public ISignatureRepository Signature { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            // Initialize repositories here
            User = new UserRepository(_db);
            Employee = new EmployeeRepository(_db);
            Feedback = new FeedbackRepository(_db);
            Goal = new GoalRepository(_db);
            Review = new ReviewRepository(_db);
            Signature = new SignatureRepository(_db);

        }

        void IUnitOfWork.Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
