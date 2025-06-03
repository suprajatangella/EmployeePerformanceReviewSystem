using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Goal
{
    public class UpdateGoalUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGoalUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ExecuteAsync(Entities.Goal goal)
        {
            var existingGoal = _unitOfWork.Goal.Get(r => r.Id == goal.Id);

            if (existingGoal == null)
                throw new ArgumentNullException(nameof(existingGoal));

            _unitOfWork.Goal.Update(goal);
            _unitOfWork.Save();
            return true;
        }
    }
}
