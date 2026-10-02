using Microsoft.EntityFrameworkCore;
using Barbearia_MVC.Models;
using Barbearia_MVC.Data;

namespace Barbearia_MVC.Repositories.Barber
{
    public class BarberRepository : IBarberRepository
    {
        private readonly ApplicationDbContext _context;

        public BarberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Barber?> GetByIdAsync(int id)
        {
            return await _context.Barbers.FindAsync(id);
        }

        // Traz absolutamente todos, inclusive os desativados (útil para relatórios históricos)
        public async Task<IEnumerable<Barber>> GetAllAsync()
        {
            return await _context.Barbers.ToListAsync();
        }

        // Traz apenas os barbeiros que estão ativos no momento para trabalhar
        public async Task<IEnumerable<Barber>> GetActiveBarbersAsync()
        {
            return await _context.Barbers
                .Where(b => b.IsActive)
                .ToListAsync();
        }

        public async Task AddAsync(Barber barber)
        {
            await _context.Barbers.AddAsync(barber);
            await _context.SaveChangesAsync();
        }

        public void Update(Barber barber)
        {
            _context.Barbers.Update(barber);
            _context.SaveChanges();
        }
    }
}
