using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TKDprogress.Models;
using TKDprogress_BLL.Interfaces;
using TKDprogress_SL.Entities;

namespace TKDprogress.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TulController : Controller
    {
        private readonly ITulService _tulService;

        public TulController(ITulService tulService)
        {
            _tulService = tulService;
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

        // GET: TulController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TulController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
