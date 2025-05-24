using EmployeePerformanceReview.Infrastructure.Data;
using EmployeePerformanceReview.Domain.Entities;
using EmployeePerformanceReview.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeePerformanceReview.Application.Repositories.Interfaces;

namespace EmployeePerformanceReview.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
      
    }
}
