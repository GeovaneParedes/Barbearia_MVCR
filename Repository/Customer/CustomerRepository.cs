using Microsoft.EntityFrameworkCore;
using Barbearia_MVC.Models;
using Barbearia_MVC.Data;

namespace Barbearia_MVC.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Busca o cliente pelo ID único do banco
        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        // Busca o cliente pelo SSN (documento), garantindo que seja uma busca exata
        public async Task<Customer?> GetBySsnAsync(string ssn)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Ssn == ssn);
        }

        // Lista todos os clientes, mas ignora o ID 1 que criamos para o "Walk-in Customer" (Cliente de Balcão)
        // Assim sua tela de listagem de clientes fica limpa, mostrando apenas clientes reais cadastrados
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Where(c => c.Id != 1) 
                .ToListAsync();
        }

        // Adiciona um novo cliente no banco
        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        // Atualiza os dados que foram modificados pelo método UpdateCustomer no Model
        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
    }
}
