using EmployeePerformanceReview.Application.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Employee
{
    public class DeleteEmployeeUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid empId)
        {
            var employee =  _unitOfWork.Employee.Get(r => r.Id == empId);

            if (employee == null)
            {
                return false; // Could not find the review
            }

            _unitOfWork.Employee.Remove(employee);
            _unitOfWork.Save();
            return true; // Successfully deleted
        }
    }
}
