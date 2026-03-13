using CafeMVC.Models;
namespace CafeMVC.Repositories
{
    public interface ICafeChainRepository
    {
        Task<IEnumerable<CafeChain>> GetAllAsync();
        Task<CafeChain?> GetByIdAsync(int id);
        Task AddAsync(CafeChain entity);
        Task UpdateAsync(CafeChain entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
