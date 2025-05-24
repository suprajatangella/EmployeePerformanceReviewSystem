using EmployeePerformanceReview.Application.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Employee
{
    public class DeleteSignatureUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSignatureUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(Guid signatureId)
        {
            var signature =  _unitOfWork.Signature.Get(r => r.Id == signatureId);

            if (signature == null)
            {
                return false; // Could not find the review
            }

            _unitOfWork.Signature.Remove(signature);
            _unitOfWork.Save();
            return true; // Successfully deleted
        }
    }
}
