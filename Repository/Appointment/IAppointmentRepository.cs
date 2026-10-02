using Barbearia_MVC.Models;

namespace Barbearia_MVC.Repositories.Appointment
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAllAsync();
        
        // Regra de Negócio: Busca a agenda de um barbeiro específico em um dia
        Task<IEnumerable<Appointment>> GetByBarberAndDateAsync(int barberId, DateTime date);
        
        Task AddAsync(Appointment appointment);
        void Update(Appointment appointment);
    }
}
