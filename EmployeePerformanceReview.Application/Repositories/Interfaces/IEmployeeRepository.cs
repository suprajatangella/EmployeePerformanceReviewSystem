using EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void Update(Employee entity);
    }
}
