using EmployeePerformanceReview.Application.UseCases.Signature;
using EmployeePerformanceReview.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePerformanceSignatureSystem.Web.Controllers
{
    [Authorize]
    public class SignatureController : Controller
    {
        private readonly CreateSignatureUseCase _createSignatureUseCase;
        private readonly UpdateSignatureUseCase _updateSignatureUseCase;
        private readonly DeleteSignatureUseCase _deleteSignatureUseCase;
        private readonly GetAllSignatureUseCase _getAllSignatureUseCase;

        public SignatureController(
            CreateSignatureUseCase createSignatureUseCase,
            UpdateSignatureUseCase updateSignatureUseCase,
            DeleteSignatureUseCase deleteSignatureUseCase,
            GetAllSignatureUseCase getAllSignatureUseCase)
        {
            _createSignatureUseCase = createSignatureUseCase;
            _updateSignatureUseCase = updateSignatureUseCase;
            _deleteSignatureUseCase = deleteSignatureUseCase;
            _getAllSignatureUseCase = getAllSignatureUseCase;
        }

        public async Task<IActionResult> Create(Signature Signature)
        {
            if (Signature == null)
                return BadRequest("Signature cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _createSignatureUseCase.ExecuteAsync(Signature);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Update(Guid id, Signature Signature)
        {
            if (id != Signature.Id)
                return BadRequest("Signature ID mismatch.");
            if (Signature == null)
                return BadRequest("Signature cannot be null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                await _updateSignatureUseCase.ExecuteAsync(Signature);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Signature ID.");
            else
            {
                var result = await _deleteSignatureUseCase.ExecuteAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Index()
        {
            var Signatures = await _getAllSignatureUseCase.ExecuteAsync();
            return View();
        }
    }
}
