using EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.Repositories.Interfaces
{
    public interface IGoalRepository : IRepository<Goal>
    {
        void Update(Goal entity);
    }
}
