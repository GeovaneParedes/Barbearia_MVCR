using Barbearia_MVC.Models;

namespace Barbearia_MVC.Repositories.Barber
{
    public interface IBarberRepository
    {
        Task<Barber?> GetByIdAsync(int id);
        Task<IEnumerable<Barber>> GetAllAsync();
        Task<IEnumerable<Barber>> GetActiveBarbersAsync(); // Regra: Trazer apenas quem está trabalhando
        Task AddAsync(Barber barber);
        void Update(Barber barber);
    }
}
