using EmployeePerformanceReview.Application.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Goal
{
    public class DeleteGoalUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGoalUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid goalId)
        {
            var goal = _unitOfWork.Goal.Get(r => r.Id == goalId);

            if (goal == null)
            {
                return false; // Could not find the review
            }

            _unitOfWork.Goal.Remove(goal);
            _unitOfWork.Save();
            return true; // Successfully deleted
        }
    }
}
