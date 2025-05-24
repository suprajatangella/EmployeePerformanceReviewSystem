using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Employee
{
    public class UpdateEmployeeUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ExecuteAsync(Entities.Employee employee)
        {
            var existingEmp = _unitOfWork.Employee.Get(r => r.Id == employee.Id);

            if (existingEmp == null)
                throw new ArgumentNullException(nameof(existingEmp));

            _unitOfWork.Employee.Update(employee);
            _unitOfWork.Save();
            return true;
        }
    }
}
