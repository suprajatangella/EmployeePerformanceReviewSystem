using EmployeePerformanceReview.Application.UseCases.Employee;
using EmployeePerformanceReview.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePerformanceReviewSystem.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CreateEmployeeUseCase _createEmployeeUseCase;
        private readonly UpdateEmployeeUseCase _updateEmployeeUseCase;
        private readonly DeleteEmployeeUseCase _deleteEmployeeUseCase;
        private readonly GetAllEmployeeUseCase _getAllEmployeeUseCase;

        public EmployeeController(
            CreateEmployeeUseCase createEmployeeUseCase,
            UpdateEmployeeUseCase updateEmployeeUseCase,
            DeleteEmployeeUseCase deleteEmployeeUseCase,
            GetAllEmployeeUseCase getAllEmployeeUseCase)
        {
            _createEmployeeUseCase = createEmployeeUseCase;
            _updateEmployeeUseCase = updateEmployeeUseCase;
            _deleteEmployeeUseCase = deleteEmployeeUseCase;
            _getAllEmployeeUseCase = getAllEmployeeUseCase;
        }

        public async Task<IActionResult> Create(Employee employee)
        {
            if (employee == null)
                return BadRequest("Employee cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _createEmployeeUseCase.ExecuteAsync(employee);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Update(Guid id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest("Employee ID mismatch.");
            if (employee == null)
                return BadRequest("Employee cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _updateEmployeeUseCase.ExecuteAsync(employee);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Employee ID.");
            else
            {
                var result = await _deleteEmployeeUseCase.ExecuteAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _getAllEmployeeUseCase.ExecuteAsync();
            return View(employees);
        }
    }
}
