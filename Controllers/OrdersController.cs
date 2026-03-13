using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CafeMVC.Models;
using CafeMVC.Repositories;
namespace CafeMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICafeChainRepository _cafeRepo;
        public OrdersController(IOrderRepository orderRepo, ICafeChainRepository cafeRepo)
        {
            _orderRepo = orderRepo;
            _cafeRepo = cafeRepo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _orderRepo.GetAllAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var item = await _orderRepo.GetByIdAsync(id.Value);
            return item == null ? NotFound() : View(item);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.CafeChainId = new SelectList(await _cafeRepo.GetAllAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderNumber,Dishes,OrderTime,Status,CafeChainId")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderRepo.AddAsync(order);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CafeChainId = new SelectList(await _cafeRepo.GetAllAsync(), "Id", "Name", order.CafeChainId);
            return View(order);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var item = await _orderRepo.GetByIdAsync(id.Value);
            if (item == null) return NotFound();
            ViewBag.CafeChainId = new SelectList(await _cafeRepo.GetAllAsync(), "Id", "Name", item.CafeChainId);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderNumber,Dishes,OrderTime,Status,CafeChainId")] Order order)
        {
            if (id != order.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _orderRepo.UpdateAsync(order);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CafeChainId = new SelectList(await _cafeRepo.GetAllAsync(), "Id", "Name", order.CafeChainId);
            return View(order);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _orderRepo.GetByIdAsync(id.Value);
            return item == null ? NotFound() : View(item);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
