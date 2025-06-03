using EmployeePerformanceReview.Application.UseCases.Goal;
using EmployeePerformanceReview.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePerformanceReviewSystem.Web.Controllers
{
    [Authorize]
    public class GoalController : Controller
    {
        private readonly CreateGoalUseCase _createGoalUseCase;
        private readonly UpdateGoalUseCase _updateGoalUseCase;
        private readonly DeleteGoalUseCase _deleteGoalUseCase;
        private readonly GetAllGoalUseCase _getAllGoalUseCase;

        public GoalController(
            CreateGoalUseCase createGoalUseCase,
            UpdateGoalUseCase updateGoalUseCase,
            DeleteGoalUseCase deleteGoalUseCase,
            GetAllGoalUseCase getAllGoalUseCase)
        {
            _createGoalUseCase = createGoalUseCase;
            _updateGoalUseCase = updateGoalUseCase;
            _deleteGoalUseCase = deleteGoalUseCase;
            _getAllGoalUseCase = getAllGoalUseCase;
        }

        public async Task<IActionResult> Create(Goal Goal)
        {
            if (Goal == null)
                return BadRequest("Goal cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _createGoalUseCase.ExecuteAsync(Goal);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Update(Guid id, Goal Goal)
        {
            if (id != Goal.Id)
                return BadRequest("Goal ID mismatch.");
            if (Goal == null)
                return BadRequest("Goal cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _updateGoalUseCase.ExecuteAsync(Goal);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Goal ID.");
            else
            {
                var result = await _deleteGoalUseCase.ExecuteAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Index()
        {
            var Goals = await _getAllGoalUseCase.ExecuteAsync();
            return View();
        }
    }
}
