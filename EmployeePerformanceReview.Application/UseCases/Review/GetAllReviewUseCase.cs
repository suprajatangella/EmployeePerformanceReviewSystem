using EmployeePerformanceReview.Application.Repositories.Interfaces;
using EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Review
{
    public class GetAllReviewUseCase
    {
        private readonly IUnitOfWork _unitOfWOrk;

        public GetAllReviewUseCase(IUnitOfWork unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public async Task<List<Entities.Review>> ExecuteAsync()
        {
            return _unitOfWOrk.Review.GetAll(includeProperties: "Employee").ToList();
        }
    }
}
