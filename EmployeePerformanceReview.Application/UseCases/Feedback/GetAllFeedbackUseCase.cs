using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Feedback
{
    public class GetAllFeedbackUseCase
    {
        private readonly IUnitOfWork _unitOfWOrk;

        public GetAllFeedbackUseCase(IUnitOfWork unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public async Task<List<Entities.Feedback>> ExecuteAsync()
        {
            return _unitOfWOrk.Feedback.GetAll(includeProperties: "Employee").ToList();
        }
    }
}
