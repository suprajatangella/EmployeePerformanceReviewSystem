using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Signature
{
    public class UpdateSignatureUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSignatureUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ExecuteAsync(Entities.Signature signature)
        {
            var existingSignature = _unitOfWork.Signature.Get(r => r.Id == signature.Id);

            if (existingSignature == null)
                throw new ArgumentNullException(nameof(existingSignature));

            _unitOfWork.Signature.Update(signature);
            _unitOfWork.Save();
            return true;
        }
    }
}
