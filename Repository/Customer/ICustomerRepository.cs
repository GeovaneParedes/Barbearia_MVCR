using Barbearia_MVC.Models;

namespace Barbearia_MVC.Repositories.Customer
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetBySsnAsync(string ssn); // Busca por SSN (útil para validações)
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        void Update(Customer customer);
    }
}
