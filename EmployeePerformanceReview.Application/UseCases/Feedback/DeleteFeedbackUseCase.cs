using EmployeePerformanceReview.Application.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Feedback
{
    public class DeleteFeedbackUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFeedbackUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid feedbackId)
        {
            var feedback =  _unitOfWork.Feedback.Get(r => r.Id == feedbackId);

            if (feedback == null)
            {
                return false; // Could not find the review
            }

            _unitOfWork.Feedback.Remove(feedback);
            _unitOfWork.Save();
            return true; // Successfully deleted
        }
    }
}
