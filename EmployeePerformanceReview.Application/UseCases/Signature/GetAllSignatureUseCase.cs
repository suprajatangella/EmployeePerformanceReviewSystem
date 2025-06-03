using EmployeePerformanceReview.Application.Repositories.Interfaces;
using EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Signature
{
    public class GetAllSignatureUseCase
    {
        private readonly IUnitOfWork _unitOfWOrk;

        public GetAllSignatureUseCase(IUnitOfWork unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public async Task<List<Entities.Signature>> ExecuteAsync()
        {
            return _unitOfWOrk.Signature.GetAll(includeProperties: "Employee").ToList();
        }
    }
}
