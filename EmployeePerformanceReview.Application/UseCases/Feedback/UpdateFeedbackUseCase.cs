using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Feedback
{
    public class UpdateFeedbackUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFeedbackUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ExecuteAsync(Entities.Feedback feedback)
        {
            var existingFeedback = _unitOfWork.Feedback.Get(r => r.Id == feedback.Id);

            if (existingFeedback == null)
                throw new ArgumentNullException(nameof(existingFeedback));

            _unitOfWork.Feedback.Update(feedback);
            _unitOfWork.Save();
            return true;
        }
    }
}
