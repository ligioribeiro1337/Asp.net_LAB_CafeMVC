using Microsoft.AspNetCore.Mvc;
using CafeMVC.Models;
using CafeMVC.Repositories;
namespace CafeMVC.Controllers
{
    public class CafeChainsController : Controller
    {
        private readonly ICafeChainRepository _repo;
        public CafeChainsController(ICafeChainRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var item = await _repo.GetByIdAsync(id.Value);
            return item == null ? NotFound() : View(item);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CuisineType,Regions,Menu,FoundedYear")] CafeChain cafeChain)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(cafeChain);
                return RedirectToAction(nameof(Index));
            }
            return View(cafeChain);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var item = await _repo.GetByIdAsync(id.Value);
            return item == null ? NotFound() : View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CuisineType,Regions,Menu,FoundedYear")] CafeChain cafeChain)
        {
            if (id != cafeChain.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _repo.UpdateAsync(cafeChain);
                return RedirectToAction(nameof(Index));
            }
            return View(cafeChain);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _repo.GetByIdAsync(id.Value);
            return item == null ? NotFound() : View(item);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
