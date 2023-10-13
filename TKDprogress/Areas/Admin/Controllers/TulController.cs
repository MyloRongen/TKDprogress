using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
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
    public class TulController : Controller
    {
        private readonly ITulService _tulService;
        private readonly IMovementService _movementService;
        private readonly ITulMovementService _tulMovementService;

        public TulController(ITulService tulService, IMovementService movementService, ITulMovementService tulMovementService)
        {
            _tulService = tulService;
            _movementService = movementService;
            _tulMovementService = tulMovementService;
        }

        public async Task<ActionResult> Index(string searchString)
        {
            List<Tul> tuls = await _tulService.GetTulsAsync(searchString);

            if (tuls.Any(t => t.ErrorMessage != null))
            {
                foreach (Tul tul in tuls)
                {
                    if (tul.ErrorMessage != null)
                    {
                        TempData["ErrorMessage"] = tul.ErrorMessage;
                    }
                }

                return View(new List<TulViewModel>());
            }

            List<TulViewModel> tulViewModels = tuls.Select(tul => new TulViewModel
            {
                Id = tul.Id,
                Name = tul.Name,
                Description = tul.Description,
            }).ToList();

            return View(tulViewModels);
        }

        public async Task<ActionResult> Details(int id)
        {
            Tul tul = await _tulMovementService.GetTulWithMovementByIdAsync(id);

            if (tul.ErrorMessage == null)
            {
                TulViewModel tulViewModel = new()
                {
                    Id = tul.Id,
                    Name = tul.Name,
                    Description = tul.Description,
                    Movements = tul.Movements.Select(movement => new Movement
                    {
                        Id = movement.Id,
                        Name = movement.Name,
                        ImageUrl = movement.ImageUrl,
                    }).ToList(),
                };

                return View(tulViewModel);
            }

            TempData["ErrorMessage"] = tul.ErrorMessage;
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Create()
        {
            List<Movement> movements = await _movementService.GetMovementsAsync("");

            List<Movement> movementViewModels = movements.Select(movement => new Movement
            {
                Id = movement.Id,
                Name = movement.Name,
                ImageUrl = movement.ImageUrl,
            }).ToList();

            CreateTulViewModel tulViewModel = new()
            {
                Movements = movementViewModels
            };

            return View(tulViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTulViewModel tulViewModel, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(tulViewModel);
            }

            Tul newTul = new()
            {
                Name = tulViewModel.Name,
                Description = tulViewModel.Description
            };

            Tul tul = await _tulService.CreateTulAsync(newTul);

            if (tul.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = tul.ErrorMessage;
                return View(tulViewModel);
            }

            try
            {
                StringValues tulMovementsJson = collection["TulMovements"];
                List<TulMovement>? tulMovements = JsonConvert.DeserializeObject<List<TulMovement>>(tulMovementsJson);

                await _tulMovementService.AttachMovementsToTulAsync(tul, tulMovements);

                TempData["StatusMessage"] = "The tul was successfully created!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            Tul tul = await _tulMovementService.GetTulWithMovementByIdAsync(id);
            List<Movement> movements = await _movementService.GetMovementsAsync("");

            UpdateTulViewModel tulViewModel = new()
            {
                Id = tul.Id,
                Name = tul.Name,
                Description = tul.Description,
                Movements = tul.Movements.Select(movement => new Movement
                {
                    Id = movement.Id,
                    Name = movement.Name,
                    ImageUrl = movement.ImageUrl,
                }).ToList(),
                MovementsChoices = movements,
            };

            return View(tulViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateTulViewModel tulViewModel, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(tulViewModel);
            }

            Tul newTul = new()
            {
                Id = tulViewModel.Id,
                Name = tulViewModel.Name,
                Description = tulViewModel.Description
            };

            newTul = await _tulService.UpdateTulAsync(newTul);

            if (newTul.ErrorMessage != null)
            {
                TempData["ErrorMessage"] = newTul.ErrorMessage;
                return View(tulViewModel);
            }

            try
            {
                StringValues tulMovementsJson = collection["TulMovements"];
                List<TulMovement>? tulMovements = JsonConvert.DeserializeObject<List<TulMovement>>(tulMovementsJson);

                if (tulMovements != null)
                {
                    await _tulMovementService.DeleteTulMovementsAsync(newTul.Id);
                    await _tulMovementService.AttachMovementsToTulAsync(newTul, tulMovements);
                }

                TempData["StatusMessage"] = "The category was successfully updated!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            Tul tul = await _tulMovementService.GetTulWithMovementByIdAsync(id);

            TulViewModel tulViewModel = new()
            {
                Id = tul.Id,
                Name = tul.Name,
                Description = tul.Description,
                Movements = tul.Movements.Select(movement => new Movement
                {
                    Id = movement.Id,
                    Name = movement.Name,
                    ImageUrl = movement.ImageUrl,
                }).ToList(),
            };

            return View(tulViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                Tul tul = await _tulMovementService.GetTulWithMovementByIdAsync(id);

                if (tul != null)
                {
                    await _tulMovementService.DeleteTulMovementsAsync(id);
                    await _tulService.DeleteTulAsync(tul);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["StatusMessage"] = "The category was successfully deleted!";
                    return View();
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return View();
            }
        }
    }
}
