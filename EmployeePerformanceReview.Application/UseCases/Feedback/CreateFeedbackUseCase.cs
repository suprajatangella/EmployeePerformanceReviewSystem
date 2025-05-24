using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Feedback
{
    public class CreateFeedbackUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFeedbackUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> ExecuteAsync(Entities.Feedback feedback)
        {
            if (feedback == null)
                throw new ArgumentNullException(nameof(feedback));

            feedback.Id = Guid.NewGuid(); // Ensure new ID is assigned (if needed)

            _unitOfWork.Feedback.Add(feedback);
            _unitOfWork.Save();

            return feedback.Id;
        }
    }
}
