using EmployeePerformanceReview.Application.UseCases.Review;
using EmployeePerformanceReview.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePerformanceReviewSystem.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly CreateReviewUseCase _createReviewUseCase;
        private readonly UpdateReviewUseCase _updateReviewUseCase;
        private readonly DeleteReviewUseCase _deleteReviewUseCase;
        private readonly GetAllReviewUseCase _getAllReviewUseCase;

        public ReviewController(
            CreateReviewUseCase createReviewUseCase,
            UpdateReviewUseCase updateReviewUseCase,
            DeleteReviewUseCase deleteReviewUseCase,
            GetAllReviewUseCase getAllReviewUseCase)
        {
            _createReviewUseCase = createReviewUseCase;
            _updateReviewUseCase = updateReviewUseCase;
            _deleteReviewUseCase = deleteReviewUseCase;
            _getAllReviewUseCase = getAllReviewUseCase;
        }

        public async Task<IActionResult> Create(Review review)
        {
            if (review == null)
                return BadRequest("Review cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _createReviewUseCase.ExecuteAsync(review);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Update(Guid id, Review review)
        {
            if (id != review.Id)
                return BadRequest("Review ID mismatch.");
            if (review == null)
                return BadRequest("Review cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _updateReviewUseCase.ExecuteAsync(review);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid review ID.");
            else
            {
                var result = await _deleteReviewUseCase.ExecuteAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _getAllReviewUseCase.ExecuteAsync();
            return View();
        }
    }
}
