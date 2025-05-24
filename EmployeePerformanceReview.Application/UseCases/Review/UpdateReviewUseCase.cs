using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Review
{
    public class UpdateReviewUseCase  
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReviewUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ExecuteAsync(Entities.Review review)
        {
            var existingReview = _unitOfWork.Review.Get(r => r.Id == review.Id);

            if (existingReview == null)
                throw new ArgumentNullException(nameof(existingReview));

            _unitOfWork.Review.Update(review);
            _unitOfWork.Save();
            return true;
        }
    }
}
