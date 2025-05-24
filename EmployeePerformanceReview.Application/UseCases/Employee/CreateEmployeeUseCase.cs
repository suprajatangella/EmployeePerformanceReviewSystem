using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Employee
{
    public class CreateEmployeeUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> ExecuteAsync(Entities.Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            employee.Id = Guid.NewGuid(); // Ensure new ID is assigned (if needed)

            _unitOfWork.Employee.Add(employee);
            _unitOfWork.Save();

            return employee.Id;
        }
    }
}
