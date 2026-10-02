using Microsoft.EntityFrameworkCore;
using Barbearia_MVC.Models;
using Barbearia_MVC.Data;

namespace Barbearia_MVC.Repositories.Service
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Busca um serviço específico pelo ID
        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        // Lista todos os serviços cadastrados na barbearia
        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        // Adiciona um novo serviço (ex: "Combo Barba + Cabelo") ao banco
        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        // Atualiza os dados do serviço (como o preço alterado pelo método UpdatePrice)
        public void Update(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();
        }
    }
}
