using EmployeePerformanceReview.Application.Repositories.Interfaces;
using EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Employee
{
    public class GetAllEmployeeUseCase
    {
        private readonly IUnitOfWork _unitOfWOrk;

        public GetAllEmployeeUseCase(IUnitOfWork unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public async Task<List<Entities.Employee>> ExecuteAsync()
        {
            return _unitOfWOrk.Employee.GetAll(includeProperties: "Employee").ToList();
        }
    }
}
