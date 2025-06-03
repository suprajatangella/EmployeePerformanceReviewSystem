using EmployeePerformanceReview.Application.UseCases.Feedback;
using EmployeePerformanceReview.Application.UseCases.Review;
using EmployeePerformanceReview.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePerformanceReviewSystem.Web.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly CreateFeedbackUseCase _createFeedbackUseCase;
        private readonly UpdateFeedbackUseCase _updateFeedbackUseCase;
        private readonly DeleteFeedbackUseCase _deleteFeedbackUseCase;
        private readonly GetAllFeedbackUseCase _getAllFeedbackUseCase;

        public FeedbackController(
            CreateFeedbackUseCase createFeedbackUseCase,
            UpdateFeedbackUseCase updateFeedbackUseCase,
            DeleteFeedbackUseCase deleteFeedbackUseCase,
            GetAllFeedbackUseCase getAllFeedbackUseCase)
        {
            _createFeedbackUseCase = createFeedbackUseCase;
            _updateFeedbackUseCase = updateFeedbackUseCase;
            _deleteFeedbackUseCase = deleteFeedbackUseCase;
            _getAllFeedbackUseCase = getAllFeedbackUseCase;
        }

        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (feedback == null)
                return BadRequest("Feedback cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _createFeedbackUseCase.ExecuteAsync(feedback);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Update(Guid id, Feedback feedback)
        {
            if (id != feedback.Id)
                return BadRequest("Feedback ID mismatch.");
            if (feedback == null)
                return BadRequest("Feedback cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _updateFeedbackUseCase.ExecuteAsync(feedback);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Feedback ID.");
            else
            {
                var result = await _deleteFeedbackUseCase.ExecuteAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Index()
        {
            var feedbacks = await _getAllFeedbackUseCase.ExecuteAsync();
            return View(feedbacks);
        }
    }
}
