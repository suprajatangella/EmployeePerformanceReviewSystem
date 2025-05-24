using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Goal
{
    public class GetAllGoalUseCase
    {
        private readonly IUnitOfWork _unitOfWOrk;

        public GetAllGoalUseCase(IUnitOfWork unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public async Task<List<Entities.Goal>> ExecuteAsync()
        {
            return _unitOfWOrk.Goal.GetAll(includeProperties: "Employee").ToList();
        }
    }
}
