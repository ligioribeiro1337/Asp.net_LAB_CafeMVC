using Microsoft.EntityFrameworkCore;
using CafeMVC.Data;
using CafeMVC.Models;
namespace CafeMVC.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) { _context = context; }
        public async Task<IEnumerable<Order>> GetAllAsync()
            => await _context.Orders.Include(o => o.CafeChain).ToListAsync();
        public async Task<Order?> GetByIdAsync(int id)
            => await _context.Orders.Include(o => o.CafeChain).FirstOrDefaultAsync(o => o.Id == id);
        public async Task AddAsync(Order entity)
        {
            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var e = await _context.Orders.FindAsync(id);
            if (e != null)
            {
                _context.Orders.Remove(e);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int id)
            => await _context.Orders.AnyAsync(o => o.Id == id);
    }
}
