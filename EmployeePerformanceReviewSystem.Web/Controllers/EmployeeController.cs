using EmployeePerformanceReview.Application.UseCases.Employee;
using EmployeePerformanceReview.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePerformanceEmployeeSystem.Web.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Create(Employee Employee)
        {
            if (Employee == null)
                return BadRequest("Employee cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _createEmployeeUseCase.ExecuteAsync(Employee);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Update(Guid id, Employee Employee)
        {
            if (id != Employee.Id)
                return BadRequest("Employee ID mismatch.");
            if (Employee == null)
                return BadRequest("Employee cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _updateEmployeeUseCase.ExecuteAsync(Employee);
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
            var Employees = await _getAllEmployeeUseCase.ExecuteAsync();
            return View();
        }
    }
}
