using Barbearia_MVC.Models;

namespace Barbearia_MVC.Repositories.Payment
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllAsync();
        
        // Regra de Negócio: Busca todos os pagamentos de um barbeiro específico em um intervalo de datas
        Task<IEnumerable<Payment>> GetByBarberAndDateRangeAsync(int barberId, DateTime startDate, DateTime endDate);
        
        Task AddAsync(Payment payment);
        void Update(Payment payment);
    }
}
