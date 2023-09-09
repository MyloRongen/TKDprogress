using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using TKDprogress.Models;
using TKDprogress.Models.CreateModels;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Services;
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

        // GET: TulController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

        // GET: TulController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TulController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TulController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TulController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
