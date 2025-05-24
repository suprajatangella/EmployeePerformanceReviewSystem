using EmployeePerformanceReview.Application.Repositories.Interfaces;
using EmployeePerformanceReview.Domain.Entities;
using EmployeePerformanceReview.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Infrastructure.Repositories
{
    public class SignatureRepository   : Repository<Signature>, ISignatureRepository
    {
        private readonly ApplicationDbContext _db;
        public SignatureRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Signature signature)
        {
            _db.Signatures.Update(signature);
        }
    }
}
