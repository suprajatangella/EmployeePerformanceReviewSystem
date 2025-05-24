using EmployeePerformanceReview.Application.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Review
{
    public class DeleteReviewUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid reviewId)
        {
            var review =  _unitOfWork.Review.Get(r => r.Id == reviewId);

            if (review == null)
            {
                return false; // Could not find the review
            }

            _unitOfWork.Review.Remove(review);
            _unitOfWork.Save();
            return true; // Successfully deleted
        }
    }
}
