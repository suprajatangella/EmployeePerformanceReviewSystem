using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Application.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IFeedbackRepository Feedback { get; }
        IGoalRepository Goal { get; }
        IReviewRepository Review { get; }
        ISignatureRepository Signature { get; }
        IUserRepository User { get; }
        void Save();
    }
}
