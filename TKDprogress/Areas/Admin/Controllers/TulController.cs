using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using TKDprogress.Models;
using TKDprogress.Models.CreateModels;
using TKDprogress.Models.UpdateModels;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Services;
using TKDprogress_DAL.Entities;
using TKDprogress_SL.Entities;

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
            List<TulDto> tuls = await _tulService.GetTulsAsync(searchString);

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
            TulDto tul = await _tulMovementService.GetTulWithMovementByIdAsync(id);

            TulViewModel tulViewModel = new()
            {
                Id = tul.Id,
                Name = tul.Name,
                Description = tul.Description,
                Movements = tul.Movements.Select(movement => new MovementDto
                {
                    Id = movement.Id,
                    Name = movement.Name,
                    ImageUrl = movement.ImageUrl,
                }).ToList(),
            };

            return View(tulViewModel);
        }

        public async Task<ActionResult> Create()
        {
            List<MovementDto> movements = await _movementService.GetMovementsAsync("");

            List<MovementDto> movementViewModels = movements.Select(movement => new MovementDto
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

            TulDto newTul = new()
            {
                Name = tulViewModel.Name,
                Description = tulViewModel.Description
            };

            TulDto tul = await _tulService.CreateTulAsync(newTul);

            try
            {
                StringValues tulMovementsJson = collection["TulMovements"];
                List<TulMovementDto>? tulMovements = JsonConvert.DeserializeObject<List<TulMovementDto>>(tulMovementsJson);

                await _tulMovementService.AttachMovementsToTulAsync(tul, tulMovements);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            TulDto tul = await _tulMovementService.GetTulWithMovementByIdAsync(id);
            List<MovementDto> movements = await _movementService.GetMovementsAsync("");

            UpdateTulViewModel tulViewModel = new()
            {
                Id = tul.Id,
                Name = tul.Name,
                Description = tul.Description,
                Movements = tul.Movements.Select(movement => new MovementDto
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

            TulDto newTul = new()
            {
                Id = tulViewModel.Id,
                Name = tulViewModel.Name,
                Description = tulViewModel.Description
            };

            _ = await _tulService.UpdateTulAsync(newTul);

            try
            {
                StringValues tulMovementsJson = collection["TulMovements"];
                List<TulMovementDto>? tulMovements = JsonConvert.DeserializeObject<List<TulMovementDto>>(tulMovementsJson);

                if (tulMovements != null)
                {
                    await _tulMovementService.DeleteTulMovementsAsync(newTul.Id);
                    await _tulMovementService.AttachMovementsToTulAsync(newTul, tulMovements);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            TulDto tul = await _tulMovementService.GetTulWithMovementByIdAsync(id);

            TulViewModel tulViewModel = new()
            {
                Id = tul.Id,
                Name = tul.Name,
                Description = tul.Description,
                Movements = tul.Movements.Select(movement => new MovementDto
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
                TulDto tul = await _tulMovementService.GetTulWithMovementByIdAsync(id);

                if (tul != null)
                {
                    await _tulMovementService.DeleteTulMovementsAsync(id);
                    await _tulService.DeleteTulAsync(tul);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
