using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Goal
{
    public class CreateGoalUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGoalUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> ExecuteAsync(Entities.Goal goal)
        {
            if (goal == null)
                throw new ArgumentNullException(nameof(goal));

            goal.Id = Guid.NewGuid(); // Ensure new ID is assigned (if needed)

            _unitOfWork.Goal.Add(goal);
            _unitOfWork.Save();

            return goal.Id;
        }
    }
}
