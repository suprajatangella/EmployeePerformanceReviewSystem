using EmployeePerformanceReview.Application.Repositories.Interfaces;
using Entities = EmployeePerformanceReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.UseCases.Review
{
    public class CreateSignatureUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSignatureUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> ExecuteAsync(Entities.Signature signature)
        {
            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            signature.Id = Guid.NewGuid(); // Ensure new ID is assigned (if needed)

            _unitOfWork.Signature.Add(signature);
            _unitOfWork.Save();

            return signature.Id;
        }
    }
}
