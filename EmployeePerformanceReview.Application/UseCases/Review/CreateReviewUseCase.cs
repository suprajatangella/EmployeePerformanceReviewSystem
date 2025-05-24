using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Review
{
    public class CreateReviewUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> ExecuteAsync(Entities.Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            review.Id = Guid.NewGuid(); // Ensure new ID is assigned (if needed)

            _unitOfWork.Review.Add(review);
            _unitOfWork.Save();

            return review.Id;
        }
    }
}
