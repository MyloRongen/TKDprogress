using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TKDprogress.Models;
using TKDprogress.Models.CreateModels;
using TKDprogress.Models.UpdateModels;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Interfaces.Services;
using TKDprogress_BLL.Services;

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
            List<Movement> movements = await _movementService.GetMovementsAsync(searchString);

            if (movements.Any(m => m.ErrorMessage != null))
            {
                foreach (Movement movement in movements)
                {
                    if (movement.ErrorMessage != null)
                    {
                        TempData["ErrorMessage"] = movement.ErrorMessage;
                    }
                }

                return View(new List<MovementViewModel>());
            }

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
            Movement movement = await _movementService.GetMovementByIdAsync(id);

            if (movement.ErrorMessage == null)
            {
                MovementViewModel movementViewModel = new()
                {
                    Id = movement.Id,
                    Name = movement.Name,
                    ImageUrl = movement.ImageUrl,
                };

                return View(movementViewModel);
            }

            TempData["ErrorMessage"] = movement.ErrorMessage;
            return RedirectToAction(nameof(Index));
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

            Movement movement = new()
            {
                Name = movementViewModel.Name,
                ImageUrl = movementViewModel.ImageUrl,
            };

            movement = await _movementService.CreateMovementAsync(movement);

            if (movement.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = movement.ErrorMessage;
                return View(movementViewModel);
            }

            TempData["StatusMessage"] = "The movement was successfully created!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            Movement movement = await _movementService.GetMovementByIdAsync(id);

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

            Movement movement = new()
            {
                Id = movementViewModel.Id,
                Name = movementViewModel.Name,
                ImageUrl = movementViewModel.ImageUrl,
            };

            movement = await _movementService.UpdateMovementAsync(movement);

            if (movement.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = movement.ErrorMessage;
                return View(movementViewModel);
            }

            TempData["StatusMessage"] = "The movement was successfully updated!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            Movement movement = await _movementService.GetMovementByIdAsync(id);

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
            try
            {
                Movement movement = await _movementService.GetMovementByIdAsync(id);
                await _movementService.DeleteMovementAsync(movement);

                TempData["StatusMessage"] = "The movement was successfully deleted!";
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
