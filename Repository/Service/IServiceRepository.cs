using Barbearia_MVC.Models;

namespace Barbearia_MVC.Repositories.Service
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(int id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task AddAsync(Service service);
        void Update(Service service);
    }
}
