using Microsoft.EntityFrameworkCore;
using CafeMVC.Data;
using CafeMVC.Models;
namespace CafeMVC.Repositories
{
    public class CafeChainRepository : ICafeChainRepository
    {
        private readonly AppDbContext _context;
        public CafeChainRepository(AppDbContext context) { _context = context; }
        public async Task<IEnumerable<CafeChain>> GetAllAsync()
            => await _context.CafeChains.Include(c => c.Orders).ToListAsync();
        public async Task<CafeChain?> GetByIdAsync(int id)
            => await _context.CafeChains.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);
        public async Task AddAsync(CafeChain entity)
        {
            _context.CafeChains.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CafeChain entity)
        {
            _context.CafeChains.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var e = await _context.CafeChains.FindAsync(id);
            if (e != null)
            {
                _context.CafeChains.Remove(e);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsAsync(int id)
            => await _context.CafeChains.AnyAsync(c => c.Id == id);
    }
}
