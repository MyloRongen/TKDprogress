using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TKDprogress.Models;
using TKDprogress.Models.CreateModels;
using TKDprogress.Models.UpdateModels;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
using TKDprogress_SL.Entities;

namespace TKDprogress.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MovementController : Controller
    {
        private readonly IMovementService _movementService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovementController(IMovementService movementService, IWebHostEnvironment webHostEnvironment)
        {
            _movementService = movementService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            List<MovementDto> movements = await _movementService.GetMovementsAsync(searchString);

            List<MovementViewModel> movementViewModels = movements.Select(movement => new MovementViewModel
            {
                Id = movement.Id,
                Name = movement.Name,
                ImageUrl = movement.ImageUrl,
            }).ToList();

            return View(movementViewModels);
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                MovementDto movement = await _movementService.GetMovementByIdAsync(id);

                MovementViewModel movementViewModel = new()
                {
                    Id = movement.Id,
                    Name = movement.Name,
                    ImageUrl = movement.ImageUrl,
                };

                return View(movementViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching category details: " + ex.Message;
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateMovementViewModel movementViewModel, IFormFile image)
        {
            string? fileName = await SaveImageAsync(image, _webHostEnvironment, null);

            if (!string.IsNullOrEmpty(fileName))
            {
                movementViewModel.ImageUrl = fileName;
            }

            if (!ModelState.IsValid)
            {
                return View(movementViewModel);
            }

            MovementDto movement = new()
            {
                Name = movementViewModel.Name,
                ImageUrl = movementViewModel.ImageUrl,
            };

            _ = await _movementService.CreateMovementAsync(movement);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            MovementDto movement = await _movementService.GetMovementByIdAsync(id);

            UpdateMovementViewModel movementViewModel = new()
            {
                Id = movement.Id,
                Name = movement.Name,
                ImageUrl = movement.ImageUrl,
            };

            return View(movementViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateMovementViewModel movementViewModel)
        {
            string? fileName = await SaveImageAsync(movementViewModel.Image, _webHostEnvironment, movementViewModel.ImageUrl);
            movementViewModel.ImageUrl = fileName;

            if (!ModelState.IsValid)
            {
                return View(movementViewModel);
            }

            MovementDto movement = new()
            {
                Id = movementViewModel.Id,
                Name = movementViewModel.Name,
                ImageUrl = movementViewModel.ImageUrl,
            };

            _ = await _movementService.UpdateMovementAsync(movement);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            MovementDto movement = await _movementService.GetMovementByIdAsync(id);

            MovementViewModel movementViewModel = new()
            {
                Id = movement.Id,
                Name = movement.Name,
                ImageUrl = movement.ImageUrl,
            };

            return View(movementViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            MovementDto movement = await _movementService.GetMovementByIdAsync(id);
            await _movementService.DeleteMovementAsync(movement);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return View();
            }
        }

        public async Task<string?> SaveImageAsync(IFormFile image, IWebHostEnvironment webHostEnvironment, string? existingImageUrl)
        {
            if (image != null && image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string webRootPath = webHostEnvironment.WebRootPath;
                string imagePath = Path.Combine(webRootPath, "Images", fileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }

            return existingImageUrl;
        }
    }
}
